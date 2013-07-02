using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Camurphy.CameraDownload
{
    public abstract class MediaFile : IComparable<MediaFile>, IEquatable<MediaFile>
    {
		private string _destinationFilename;

        public string Filename { get; set; }
        public long Filesize { get; set; }
        public virtual DateTime Timestamp { get; set; }
		public string DestinationFilename
		{
			get
			{
				if (_destinationFilename == null)
					return Filename;
				else
					return _destinationFilename;
			}
			set
			{
				_destinationFilename = value;
			}
		}
		public string DestinationPath { get; set; }
        public abstract void Transfer();

        #region IComparable<MediaFile> Members

        public int CompareTo(MediaFile other)
        {
 	        return Timestamp.CompareTo(other.Timestamp);
        }

        #endregion

        #region IEquatable<MediaFile> Members

        public abstract bool Equals(MediaFile other);

        #endregion
    }
    
    public class VideoFile : MediaFile
    {
        public string Path { get; set; }

        public override void  Transfer()
        {
            if (!Program.IsValidPath(DestinationPath))
                Directory.CreateDirectory(DestinationPath);

            File.Copy(Path, DestinationPath + DestinationFilename);
        }

        public override bool Equals(MediaFile other)
        {
            if (Filesize.Equals(other.Filesize) && Timestamp.Equals(other.Timestamp))
                return true;

            return false;
        }
    }

    public class ImageFile : MediaFile
    {
        public WIA.Item WIAItem { get; set; }
        public WIA.ImageFile WIAImage { get; set; }
		public bool ResizeOnTransfer { get; set; }
		public ImageSize ResizedResolution { get; set; }
        public string EquipMake { get; set; }
        public string EquipModel { get; set; }

        public override void Transfer()
        {
            if (WIAImage != null)
            {
                try
                {
                    if (!Program.IsValidPath(DestinationPath))
                        Directory.CreateDirectory(DestinationPath);

                    ProcessImage(WIAImage);
                }
                catch
                {
                    // If unable to read file abort
                    return;
                }
            }
            else
            {
                // Should not need to do this. When I try to apply a filter to an image
                // that has just been transferred from the camera using Item.Transfer
                // I always get "Error HRESULT E_FAIL has been returned from a call to a COM
                // component". Current solution is to save image file with an underscore
                // at the end of the filename, load it back into memory with LoadFile,
                // process image and then delete original file.

                WIAImage = (WIA.ImageFile)WIAItem.Transfer(Properties.Resources.WiaJPEGFormatID);

                if (!Program.IsValidPath(DestinationPath))
				    Directory.CreateDirectory(DestinationPath);

                WIAImage.SaveFile(DestinationPath + DestinationFilename + "_");

                WIAImage = new WIA.ImageFile();
                WIAImage.LoadFile(DestinationPath + DestinationFilename + "_");
                ProcessImage(WIAImage);
                File.Delete(DestinationPath + DestinationFilename + "_");
            }
        }

        private void ProcessImage(WIA.ImageFile aImage)
        {
            WIA.ImageProcess ip = new WIA.ImageProcess();

            object o = "Orientation", p = null;
            string orientation = aImage.Properties.get_Item(ref o).get_Value().ToString();

            Debug.WriteLine(orientation);

            if ((orientation == "6") || (orientation == "8"))
            {
                o = "RotateFlip";
                ip.Filters.Add(ip.FilterInfos.get_Item(ref o).FilterID, 0);
                o = "RotationAngle";
                if (orientation == "6") p = 90;
                else if (orientation == "8") p = 270;
                ip.Filters[1].Properties.get_Item(ref o).set_Value(ref p);

                aImage = ip.Apply(aImage);
                ip.Filters.Remove(1);

                int i = ip.Filters.Count;
            }

            o = "Exif";
            ip.Filters.Add(ip.FilterInfos.get_Item(ref o).FilterID, 0);
            o = "ID";
            p = 274;
            ip.Filters[1].Properties.get_Item(ref o).set_Value(ref p);
            o = "Value";
            p = null;
            ip.Filters[1].Properties.get_Item(ref o).set_Value(ref p);

            aImage = ip.Apply(aImage);
            ip.Filters.Remove(1);

            aImage.SaveFile(DestinationPath + DestinationFilename);

            if (ResizeOnTransfer)
            {
                o = "Scale";
                ip.Filters.Add(ip.FilterInfos.get_Item(ref o).FilterID, 0);
                o = "MaximumWidth";

                if (ResizedResolution == ImageSize.Small)
                    p = 640;
                else if (ResizedResolution == ImageSize.Medium)
                    p = 800;
                else if (ResizedResolution == ImageSize.Large)
                    p = 1024;

                ip.Filters[1].Properties.get_Item(ref o).set_Value(ref p);
                o = "MaximumHeight";
                ip.Filters[1].Properties.get_Item(ref o).set_Value(ref p);

                aImage = ip.Apply(aImage);

                if (!Program.IsValidPath(DestinationPath + "Small"))
                    Directory.CreateDirectory(DestinationPath + "Small");
                aImage.SaveFile(DestinationPath + "Small\\" + DestinationFilename);
            }
        }

        public override bool Equals(MediaFile other)
        {
            ImageFile otherImage = (ImageFile)other;

            if (EquipMake.Equals(otherImage.EquipMake) && EquipModel.Equals(otherImage.EquipModel) &&
                Timestamp.Equals(otherImage.Timestamp))
                return true;

            return false;
        }
    }

	public static class MediaFactory
	{
        public static ImageFile LoadImageFile(string aPath)
        {
            ImageFile image = new ImageFile();
            WIA.ImageFile wiaImage = new WIA.ImageFile();

            try
			{
				wiaImage.LoadFile(aPath);
			}
			catch
			{
				// If unable to read file ignore
				return null;
			}

            image.WIAImage = wiaImage;
            FileInfo fi = new FileInfo(aPath);
            image.Filename = fi.Name;
            image.Filesize = fi.Length;            

            object o = "EquipMake";
            image.EquipMake = wiaImage.Properties.get_Item(ref o).get_Value().ToString();
            o = "EquipModel";
            image.EquipModel = wiaImage.Properties.get_Item(ref o).get_Value().ToString();
            o = "DateTime";
            string sDateTime = wiaImage.Properties.get_Item(ref o).get_Value().ToString();

            image.Timestamp = new DateTime(Int32.Parse(sDateTime.Substring(0, 4)), 
                Int32.Parse(sDateTime.Substring(5, 2)), Int32.Parse(sDateTime.Substring(8, 2)),
                Int32.Parse(sDateTime.Substring(11, 2)), Int32.Parse(sDateTime.Substring(14, 2)),
                Int32.Parse(sDateTime.Substring(17, 2)));

            return image;
        }

		public static VideoFile LoadVideoFile(string aPath)
		{
			FileInfo fi = new FileInfo(aPath);
			return new VideoFile
			{
				Filename = fi.Name,
				Filesize = fi.Length,
				Timestamp = fi.LastWriteTimeUtc,
				Path = fi.FullName
			};
		}
	}
}