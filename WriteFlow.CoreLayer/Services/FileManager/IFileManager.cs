using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.CoreLayer.Services.FileManager
{
    public interface IFileManager
    {
        string SaveImage(IFormFile file, string savePath);
        string SaveFile(IFormFile file, string savePath);
        void DeleteFile(string fileName, string path);
    }


}
