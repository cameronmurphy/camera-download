using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Camurphy.CameraDownload
{
	public abstract class MediaLibrary
	{
		protected MediaFileList _files;
		protected string _location;
		public abstract void ScanLibrary();
        public bool ResizeOnTransfer { get; set; }
        public ImageSize ResizedResolution { get; set; }

		public MediaLibrary(string aLocation)
		{
			_location = aLocation;
			_files = new MediaFileList();
		}

		public void RemoveExistingMedia(MediaFileList aDeviceFiles)
		{
            MediaFileList remQueue = new MediaFileList();

            foreach (MediaFile smf in aDeviceFiles)
            {
                foreach (MediaFile dmf in _files)
                {
                    if (smf.Equals(dmf))
                    {
                        remQueue.Add(smf);
                        break;
                    }
                }
            }

            foreach (MediaFile smf in remQueue)
                aDeviceFiles.Remove(smf);
		}

        public virtual int GetNextID(string aPrefix, int aDigits)
        {
            List<string> filenames = new List<string>();

            foreach (MediaFile file in _files)
                filenames.Add(file.Filename);

            return GetNextID(aPrefix, aDigits, filenames);
        }

		public int GetNextID(string aPrefix, int aDigits, List<string> files)
        {
            int max = -1;

            foreach (string file in files)
            {
                int temp;

                // Remove extension from filename
                string fnTrunc = file.Substring(0, file.LastIndexOf("."));

                // If prefix is less than length of length of filename (cannot be equal to because of numbering)
                if (aPrefix.Length < fnTrunc.Length)
                    // If beginning of filename is equal to prefix
                    if (fnTrunc.Substring(0, aPrefix.Length).ToUpper() == aPrefix.ToUpper())
                        // If length of remainder of filename is equal to the number of leading zeros
                        if (fnTrunc.Length - aPrefix.Length == aDigits)
                            // Try to parse remaining characters as integer into temp
                            if (int.TryParse(fnTrunc.Substring(aPrefix.Length, aDigits), out temp))
                                if (temp > max)
                                    max = temp;
            }

            if (max == -1)
                return -1;
            else
                return max + 1;
		}

		public virtual void Add(MediaFile aFile)
		{
			aFile.Transfer();
		}

        public virtual void Save() { }
    }

	public class VideoLibrary : MediaLibrary
	{
		public VideoLibrary(string aLocation) : base(aLocation) { }

		public override void ScanLibrary()
		{
			ScanDirectory(_location);
		}

		private void ScanDirectory(string aPath)
		{
			DirectoryInfo dirInfo = new DirectoryInfo(aPath);

			foreach (DirectoryInfo d in dirInfo.GetDirectories())
				ScanDirectory(d.FullName);

			foreach (FileInfo f in dirInfo.GetFiles())
			{
				if (f.Extension.Length > 0)
				{
					string ext = f.Extension.Substring(1, f.Extension.Length - 1);

					if (ext.ToUpper() == "MOD")
						_files.Add(MediaFactory.LoadVideoFile(f.FullName));
				}
			}
		}
	}

	public class ImageLibrary : MediaLibrary
	{
        private StreamWriter _libraryWriter;
        public ImageLibrary(string aLocation) : base(aLocation) { }
        bool _libraryExists = false;

        private string LibraryPath
        {
            get
            {
                return _location + "library.csv";
            }
        }

		public override void ScanLibrary()
		{
            if (File.Exists(LibraryPath))
            {
                _libraryExists = true;

                StreamReader libraryReader = new StreamReader(LibraryPath);

                while (libraryReader.Peek() >= 0)
                {
                    string[] line = libraryReader.ReadLine().Split(","[0]);

                    if (line.Length == 3)
                        _files.Add(new ImageFile { EquipMake = line[0], EquipModel = line[1], Timestamp = DateTime.Parse(line[2]) });
                }

                libraryReader.Close();
            }
		}

		public override void Add(MediaFile aFile)
		{
            ImageFile image = (ImageFile)aFile;

            image.ResizeOnTransfer = ResizeOnTransfer;
            image.ResizedResolution = ResizedResolution;

			base.Add(aFile);

            if (_libraryWriter == null)
            {
                _libraryWriter = new StreamWriter(LibraryPath, true);

                if (!_libraryExists)
                    File.SetAttributes(LibraryPath, File.GetAttributes(LibraryPath) | FileAttributes.Hidden);
            }

            _libraryWriter.WriteLine(String.Format("{0},{1},{2}", image.EquipMake, image.EquipModel, image.Timestamp.ToString()));
		}

        public override void Save()
        {
            _libraryWriter.Close();
        }

        public override int GetNextID(string aPrefix, int aDigits)
        {
            List<string> files = new List<string>();
            ScanDirectory(_location, ref files);

            return base.GetNextID(aPrefix, aDigits, files);
        }

        private void ScanDirectory(string aPath, ref List<string> aFiles)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(aPath);

            foreach (DirectoryInfo d in dirInfo.GetDirectories())
                ScanDirectory(d.FullName, ref aFiles);

            foreach (FileInfo f in dirInfo.GetFiles())
            {
                if (f.Extension.Length > 0)
                {
                    string ext = f.Extension.Substring(1, f.Extension.Length - 1);

                    if (ext.ToUpper() == "JPG")
                        aFiles.Add(f.Name);
                }
            }
        }
	}
}
