using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Threading;

namespace Camurphy.UpdateService
{
	public partial class DownloadDialog : Form
	{
		private string m_Uri, m_SavePath;
		private BackgroundWorker m_Worker;

		public DownloadDialog(string uri, string savePath)
		{
			InitializeComponent();

			m_Uri = uri;
			m_SavePath = savePath;
		}

		private void DownloadDialog_Load(object sender, EventArgs e)
		{
			lblUri.Text = m_Uri;

			m_Worker = new BackgroundWorker();
			m_Worker.WorkerReportsProgress = true;
			m_Worker.WorkerSupportsCancellation = true;
			m_Worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChangedHandler);
			m_Worker.DoWork += new DoWorkEventHandler(DoWorkHandler);
			m_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);

			DownloadWorkerSettings settings = new DownloadWorkerSettings();
			settings.URI = m_Uri;
			settings.SavePath = m_SavePath;

			m_Worker.RunWorkerAsync(settings);
		}

		private void ProgressChangedHandler(object sender, ProgressChangedEventArgs e)
		{
			pgbProgress.Value = e.ProgressPercentage;

			DownloadWorkerProgress progress = (DownloadWorkerProgress)e.UserState;

			lblProgressKB.Text = String.Format("{0}kb / {1}kb", progress.KilobytesProgress,
				progress.KilobytesTotal);
			lblProgressPercent.Text = e.ProgressPercentage + "%";
		}

		private void DoWorkHandler(object sender, DoWorkEventArgs e)
		{
			DownloadWorkerSettings settings = (DownloadWorkerSettings)e.Argument;
			BackgroundWorker worker = sender as BackgroundWorker;

			// Determine size of file with a request
			WebRequest request = HttpWebRequest.Create(settings.URI);
			WebResponse response;

			try
			{
				response = request.GetResponse();
			}
			catch (WebException)
			{
				MessageBox.Show("Error downloading file");
				e.Cancel = true;
				return;
			}

			response.Close();

			Int64 totalBytes = response.ContentLength, currentBytes = 0;

			WebClient client = new WebClient();
			Stream remote = client.OpenRead(settings.URI);
			Stream local = new FileStream(settings.SavePath, FileMode.Create, FileAccess.Write, FileShare.None);

			int byteSize = 0;
			byte[] byteBuffer = new byte[totalBytes];

			DownloadWorkerProgress progress = new DownloadWorkerProgress
			{
				KilobytesTotal = (int)Math.Ceiling(totalBytes / (double)1024)
			};

			while ((byteSize = remote.Read(byteBuffer, 0, 4096)) > 0)
			{
				if (worker.CancellationPending)
				{
					remote.Close();
					local.Close();
					File.Delete(settings.SavePath);
					e.Cancel = true;
					return;
				}

				local.Write(byteBuffer, 0, byteSize);
				currentBytes += byteSize;

				double index = (double)currentBytes;
				double total = (double)totalBytes;
				int progressPercent = (int)Math.Ceiling((index / total) * 100);

				progress.KilobytesProgress = (int)Math.Ceiling(currentBytes / (double)1024);

				worker.ReportProgress(progressPercent, progress);
			}

			e.Result = true;

			local.Close();
			remote.Close();
		}

		private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if ((bool)e.Cancelled)
			{
				DialogResult = DialogResult.Cancel;
			}
			else if ((bool)e.Result)
			{
				DialogResult = DialogResult.Yes;
			}
		}

		private void DownloadDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (m_Worker != null)
				if (m_Worker.IsBusy)
					m_Worker.CancelAsync();
		}
	}

	struct DownloadWorkerSettings
	{
		public string URI { get; set; }
		public string SavePath { get; set; }
	}

	struct DownloadWorkerProgress
	{
		public int KilobytesProgress { get; set; }
		public int KilobytesTotal { get; set; }
	}
}
