using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Camurphy.CameraDownload
{
    public class MediaFileList : List<MediaFile>
    {
        public long SumFileSizes()
        {
            long total = 0;

            foreach (MediaFile file in this)
                total += file.Filesize;

            return total;
        }

        public bool FilesExist()
        {
            foreach (MediaFile file in this)
                if (File.Exists(file.DestinationPath + file.DestinationFilename))
                    return true;

            return false;
        }
    }
}
