using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EveryCent.Services;
using System.IO;

namespace EveryCent.Droid.Platform
{
    public class LocalPath : ILocalPath
    {
        public string GetLocalPath
        {
            get
            {
                return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
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
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);
        }

        public Stream GetLocalStreamFilePath(string filename)
        {
            throw new NotImplementedException();
        }

        public void SavePicture(string filename, byte[] imagedata)
        {
            string filePath = Path.Combine(GetLocalPath, filename);
            File.WriteAllBytes(filePath, imagedata);
        }
    }
}