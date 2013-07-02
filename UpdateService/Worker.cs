using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Net;
using System.Diagnostics;
using System.Xml.XPath;
using System.IO;
using System.Net.Cache;

namespace Camurphy.UpdateService
{
	public sealed class Worker
	{
		#region Singleton implementation

		static readonly Worker instance = new Worker();

		static Worker() { }

		Worker() { }

		public static Worker Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion Singleton implementation

		private BackgroundWorker m_UpdateWorker;
		private ConfirmDialog m_ConfirmDialog;
		private DownloadDialog m_DownloadDialog;

		public void CheckForUpdates()
		{
			m_UpdateWorker = new BackgroundWorker();
			m_UpdateWorker.DoWork += new DoWorkEventHandler(DoWorkHandler);
			m_UpdateWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompletedHandler);
			m_UpdateWorker.RunWorkerAsync();
		}

		private void DoWorkHandler(object sender, DoWorkEventArgs e)
		{
			// Substitute application name and replace spaces with hyphens
			WebRequest request = HttpWebRequest.Create(Properties.Resources.UpdatePath.Replace("<application>",
				System.Windows.Forms.Application.ProductName.ToLower().Replace(" ", "-")));

			WebResponse response;

			try
			{
				response = request.GetResponse();
			}
			catch (WebException)
			{
				// Request failed, abort
				e.Result = new UpdateWorkerResult { NewVersionAvailable = false };
				return;
			}

			Stream responseSteam = response.GetResponseStream();

			XPathDocument document = new XPathDocument(responseSteam);
			XPathNavigator navigator = document.CreateNavigator();
			
			XPathNodeIterator iterator = navigator.Select("/application/latestVersion");
			iterator.MoveNext();
			ProductVersion newVersion = new ProductVersion(iterator.Current.Value);
			ProductVersion oldVersion = new ProductVersion(System.Windows.Forms.Application.ProductVersion);

			UpdateWorkerResult result = new UpdateWorkerResult();
			result.NewVersionAvailable = newVersion.CompareTo(oldVersion) > 0;

			iterator = navigator.Select("/application/downloadUri");
			iterator.MoveNext();
			result.DownloadUri = iterator.Current.Value;

			e.Result = result;

			response.Close();
		}

		private void RunWorkerCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
		{
			UpdateWorkerResult result = (UpdateWorkerResult)e.Result;

			if (result.NewVersionAvailable)
			{
				m_ConfirmDialog = new ConfirmDialog(System.Windows.Forms.Application.ProductName);
				DialogResult confirmDialogResult = m_ConfirmDialog.ShowDialog();

				if (confirmDialogResult == DialogResult.Yes)
				{
					// Extract filename from URI
					string[] uri = result.DownloadUri.Split('/');
					string filename = uri[uri.Length - 1];

					string savePath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User)
						+ "\\" + filename;

					// Start download
					m_DownloadDialog = new DownloadDialog(result.DownloadUri, savePath);
					DialogResult downloadDialogResult = m_DownloadDialog.ShowDialog();

					if (downloadDialogResult == DialogResult.Yes)
					{
						Process.Start(savePath, "/passive");
						Application.Exit();
					}
				}
			}
		}
	}

	struct UpdateWorkerResult
	{
		public bool NewVersionAvailable { get; set; }
		public string DownloadUri { get; set; }
	}
}