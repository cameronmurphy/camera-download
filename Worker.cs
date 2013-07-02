using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace Camurphy.CameraDownload
{
    public sealed class Worker
    {
        private enum WorkerResult { Completed = 0, NoNewFiles = 1 }

        static Worker instance = null;
        static readonly object padlock = new object();

        public WorkerSettings Settings { get; set; }

        private TransferDialog _transferDialog;

        private BackgroundWorker _bgWorker;

        Worker() { }

        public static Worker Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new Worker();

                    return instance;
                }
            }
        }

        public void Begin()
        {
            if (_bgWorker != null)
                if (_bgWorker.IsBusy)
                    throw new ApplicationException("Worker thread already running");

            _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerReportsProgress = true;
            _bgWorker.WorkerSupportsCancellation = true;
            _bgWorker.DoWork += new DoWorkEventHandler(DoWorkHandler);
            _bgWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressChangedHandler);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkCompletedHandler);

            _transferDialog = new TransferDialog();
            _transferDialog.Show();

            _bgWorker.RunWorkerAsync(Settings);
        }

        public void Cancel()
        {
            _bgWorker.CancelAsync();
        }

        private void DoWorkHandler(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            WorkerSettings settings = (WorkerSettings)e.Argument;
			MediaLibrary library;

            if (settings.MediaType == MediaType.Video)
            {
                library = new VideoLibrary(settings.DestinationDirectory);
            }
            else
            {
                library = new ImageLibrary(settings.DestinationDirectory);
                library.ResizeOnTransfer = settings.ResizeImages;
                library.ResizedResolution = settings.ImageSize;
            }

            int numberFrom = 1;

            //Loop through all the steps of the operation, stop if cancelled
            for (int i = 0; i <= 6; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                switch (i)
                {
                    case 0:
                        _bgWorker.ReportProgress(-1, "Scanning device");
                        settings.Device.ScanDevice(settings.MediaType);
                        if (settings.Device.Files.Count == 0)
                            throw new Exception(Properties.Resources.NoFilesError);
                        break;

                    case 1:
                        _bgWorker.ReportProgress(-1, "Scanning destination directory");
						library.ScanLibrary();
                        break;

                    case 2:
                        _bgWorker.ReportProgress(-1, "Comparing");
						library.RemoveExistingMedia(settings.Device.Files);
                        if (settings.Device.Files.Count == 0)
                        {
                            e.Result = WorkerResult.NoNewFiles;
                            return;
                        }
                        break;

                    case 3:
                        if (!settings.RenameFiles && settings.Device.HasDuplicateFilenames())
                            throw new Exception(Properties.Resources.DuplicateFilesError);
                        break;

                    case 4:
						_bgWorker.ReportProgress(-1, "Working...");
                        settings.Device.Files.Sort();

                        if (settings.RenameFiles && settings.ResumeNumbering)
                        {
							int next = library.GetNextID(settings.FilePrefix, settings.FileDigits);
                            if (next == -1)
                            {
								if (MessageBox.Show(Properties.Resources.ResumeNumberingWarning,
									"Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
								{
									e.Cancel = true;
									return;
								}
                            }
                            else
								numberFrom = next;

                            if (numberFrom + settings.Device.Files.Count - 1 > Math.Pow(10, settings.FileDigits))
                                throw new Exception(Properties.Resources.DigitShortageError);
                        }

                        break;

					case 5:
						DateTime prevDate = DateTime.MinValue;

						foreach (MediaFile file in settings.Device.Files)
						{
							if ( !prevDate.Date.Equals(file.Timestamp.Date) && settings.RenameFiles &&
								settings.RestartNumbering )
							{
								numberFrom = 1;
								prevDate = file.Timestamp;
							}

							file.DestinationPath = settings.DestinationDirectory +
								file.Timestamp.ToString("yyyy_MM_dd") + "\\";

							if (settings.RenameFiles)
							{
								file.DestinationFilename = settings.FilePrefix + numberFrom.ToString("D" + settings.
									FileDigits.ToString()) + file.Filename.Substring(file.Filename.LastIndexOf("."),
									file.Filename.Length - file.Filename.LastIndexOf("."));
								numberFrom++;
							}
						}

						if (settings.Device.Files.FilesExist())
							throw new Exception(Properties.Resources.FileExistsError);

						break;

                    case 6:
                        

						_bgWorker.ReportProgress(0, "Beginning file transfer");
                        long totalBytes = settings.Device.Files.SumFileSizes(), progressBytes = 0;
						int remainingFiles = settings.Device.Files.Count;

                        foreach (MediaFile file in settings.Device.Files)
                        {
                            if (worker.CancellationPending)
                            {
                                library.Save();
                                e.Cancel = true;
                                return;
                            }

							_bgWorker.ReportProgress(-1, String.Format("Transferring files for {0}. {1} files remaining",
								file.Timestamp.ToShortDateString(), remainingFiles.ToString()));

                            library.Add(file);
							remainingFiles--;
							progressBytes += file.Filesize;

							if (totalBytes > 0)
								_bgWorker.ReportProgress(Convert.ToInt32(progressBytes * 100 / totalBytes));
                        }

                        break;
                }
            }

            library.Save();
            e.Result = WorkerResult.Completed;
        }

        private void ProgressChangedHandler(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= 0)
            {
                if (e.ProgressPercentage >= 100)
					_transferDialog.Progress = 100;
                else
                    _transferDialog.Progress = e.ProgressPercentage;
            }

			if (e.UserState != null)
				_transferDialog.StatusBarText = e.UserState.ToString();
        }

        private void WorkCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(_transferDialog, e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Do nothing, e.Cancel needs to be tested before e.Result or TargetInvocationException is thrown
            }
            else if (e.Result != null)
            {
                if ((WorkerResult)e.Result == WorkerResult.NoNewFiles)
                    MessageBox.Show(_transferDialog, "No new files to copy");
                else if ((WorkerResult)e.Result == WorkerResult.Completed)
                    MessageBox.Show(_transferDialog, "Completed");
            }

            _transferDialog.Hide();
            Program.MainForm.Show();
        }
    }

    public struct WorkerSettings
    {
        private string _destinationDirectory;

        public MediaDevice Device { get; set; }
        public string DestinationDirectory
        {
            get
            {
                return _destinationDirectory;
            }
            set
            {
                _destinationDirectory = value;

                if ((_destinationDirectory[_destinationDirectory.Length - 1] != "\\"[0])
                    && (_destinationDirectory[_destinationDirectory.Length - 1] != "/"[0]))
                    _destinationDirectory += "\\";
            }
        }
        public MediaType MediaType { get; set; }
        public bool ResizeImages { get; set; }
        public ImageSize ImageSize { get; set; }
        public bool RenameFiles { get; set; }
        public bool ResumeNumbering { get; set; }
        public bool RestartNumbering { get; set; }
        public string FilePrefix { get; set; }
        public int FileDigits { get; set; }
    }
}