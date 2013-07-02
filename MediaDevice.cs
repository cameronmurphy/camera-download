using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Camurphy.CameraDownload
{
    public abstract class MediaDevice
    {
        public MediaFileList Files { get; set; }

        public MediaDevice()
        {
            Files = new MediaFileList();
        }

		public abstract void ScanDevice(MediaType aMediaType);

		public bool HasDuplicateFilenames()
		{
			for (int i = 0; i < Files.Count; i++)
			{
				for (int j = i + 1; j < Files.Count; j++)
				{
					if ((Files[i].Filename.ToUpper() == Files[j].Filename.ToUpper()) &&
						Files[i].Timestamp.Date.Equals(Files[j].Timestamp.Date))
						return true;
				}
			}

			return false;
		}
    }

    public class MassStorageDevice : MediaDevice
    {
        private string _driveLetter;
        private string _volumeLabel;

        public MassStorageDevice(string aDriveLetter, string aVolumeLabel)
            : base()
        {
            _driveLetter = aDriveLetter;
            _volumeLabel = aVolumeLabel;
        }

        public override void ScanDevice(MediaType aMediaType)
        {
			Files.Clear();
			ScanDirectory(_driveLetter, aMediaType);
        }

		public void ScanDirectory(string aPath, MediaType aMediaType)
		{
			DirectoryInfo dirInfo = new DirectoryInfo(aPath);

			foreach (DirectoryInfo d in dirInfo.GetDirectories())
				ScanDirectory(d.FullName, aMediaType);

			foreach (FileInfo f in dirInfo.GetFiles())
			{
				if (f.Extension.Length > 0)
				{
					string ext = f.Extension.Substring(1, f.Extension.Length - 1);

					if ((ext.ToUpper() == "MOD") && (aMediaType == MediaType.Video))
						Files.Add(MediaFactory.LoadVideoFile(f.FullName));
					else if ((ext.ToUpper() == "JPG") && (aMediaType == MediaType.Image))
						Files.Add(MediaFactory.LoadImageFile(f.FullName));
				}
			}
		}

        public override string ToString()
        {
            if (_volumeLabel.Length > 0)
                return _driveLetter + " - " + _volumeLabel;
            else
                return _driveLetter;
        }
    }

    public class StillCamera : MediaDevice
    {
        private WIA.DeviceInfo _cameraInfo;
        private WIA.Device _camera;

        public StillCamera(WIA.DeviceInfo aCameraInfo)
        {
            _cameraInfo = aCameraInfo;
        }

		public override void ScanDevice(MediaType aMediaType)
        {
            bool firstImage = true;
            string equipMake = null, equipModel = null;

            Files.Clear();

            if (_camera == null)
            {
                try
                {
                    _camera = _cameraInfo.Connect();
                }
                catch
                {
                    throw new Exception(Properties.Resources.CameraConnectError);
                }
            }

            if (_camera.Items.Count > 0)
            {                
                foreach (WIA.Item item in _camera.Items)
                {
                    object o = "Item Flags", p;

                    if (((WIA.WiaItemFlag)item.Properties.get_Item(ref o).get_Value() &
                        WIA.WiaItemFlag.ImageItemFlag) == WIA.WiaItemFlag.ImageItemFlag)
                    {
                        if (firstImage)
                        {
                            WIA.ImageFile wiaImage = (WIA.ImageFile)item.Transfer(Properties.Resources.WiaJPEGFormatID);
                            o = "EquipMake";
                            equipMake = wiaImage.Properties.get_Item(ref o).get_Value().ToString();
                            o = "EquipModel";
                            equipModel = wiaImage.Properties.get_Item(ref o).get_Value().ToString();
                            firstImage = false;
                        }

                        ImageFile image = new ImageFile();
                        image.EquipMake = equipMake;
                        image.EquipModel = equipModel;
                        o = "Item Name";
                        p = "Filename extension";
                        image.Filename = item.Properties.get_Item(ref o).get_Value().ToString() + "." +
                            item.Properties.get_Item(ref p).get_Value().ToString();
                        o = "Item Size";
                        image.Filesize = Int64.Parse(item.Properties.get_Item(ref o).get_Value().ToString());
                        o = "Item Time Stamp";
                        image.Timestamp = (item.Properties.get_Item(ref o).get_Value() as WIA.Vector).Date;
                        image.WIAItem = item;

                        Files.Add(image);
                    }
                }
            }
        }

        public override string ToString()
        {
            object o = "Name";
            return _cameraInfo.Properties.get_Item(ref o).get_Value().ToString();
        }
    }

    public static class DeviceFactory
    {
        public static List<MediaDevice> GetAllDevices()
        {
            List<MediaDevice> devices = new List<MediaDevice>();
            
            foreach (string s in Directory.GetLogicalDrives())
            {
                try
                {
                    DriveInfo di = new DriveInfo(s);

                    if (di.DriveType == DriveType.Removable)
                        devices.Add(new MassStorageDevice(s, di.VolumeLabel));
                }
                catch
                {
                    // If device is not ready don't add to list of devices
                }
            }

            WIA.DeviceManager devMgr = new WIA.DeviceManager();

            foreach (WIA.DeviceInfo devInfo in devMgr.DeviceInfos)
            {
                try
                {
                    WIA.Device dev = devInfo.Connect();

                    if (dev.Type == WIA.WiaDeviceType.CameraDeviceType)
                        devices.Add(new StillCamera(devInfo));
                }
                catch
                {
                    // If unable to connect don't add to list of devices
                }
            }

            return devices;
        }
    }
}
