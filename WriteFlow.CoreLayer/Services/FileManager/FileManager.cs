using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.Utilities;

namespace WriteFlow.CoreLayer.Services.FileManager
{
    public class FileManager : IFileManager
    {

        public void DeleteFile(string fileName, string path)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
            if (File.Exists(filepath))
                File.Delete(filepath);
        }

        public string SaveFile(IFormFile file, string savePath)
        {
            if (file == null)
                throw new Exception("File is null. ");

            var fileName = $"{Guid.NewGuid()}{file.FileName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }

        public string SaveImage(IFormFile file, string savePath)
        {
            var isNotImage = !ImageValidation.Validate(file.FileName);
            if (isNotImage)
                throw new Exception();

            return SaveFile(file, savePath);
        }
    }
}
