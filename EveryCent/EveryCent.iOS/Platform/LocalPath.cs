using EveryCent.Services;
using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UIKit;

namespace EveryCent.iOS.Platform
{
    public class LocalPath : ILocalPath
    {
        public string GetLocalPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
        }

        public bool FileExists(string filename)
        {
            bool ret = false;
            if (File.Exists(GetLocalFilePath(filename)))
                ret = true;
            return ret;
        }

        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);
        }

        public Stream GetLocalStreamFilePath(string filename)
        {
            throw new NotImplementedException();
        }

        public void SavePicture(string filename, byte[] imagedata)
        {
            NSError error = null;
            var image = new UIImage(NSData.FromArray(imagedata));
            NSData imageData = image.AsPNG();
            imageData.Save(Path.Combine(GetLocalPath, filename), false, out error);
        }
    }
}
