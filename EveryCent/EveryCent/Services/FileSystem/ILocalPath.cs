using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Services
{
    public interface ILocalPath
    {
        string GetLocalPath { get; }
        string GetLocalFilePath(string filename);
        Stream GetLocalStreamFilePath(string filename);
        bool FileExists(string filename);
        void SavePicture(string filename, byte[] imagedata);
    }
}
