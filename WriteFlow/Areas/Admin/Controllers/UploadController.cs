using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.Services.FileManager;
using WriteFlow.CoreLayer.Utilities;

namespace WriteFlow.Areas.Admin.Controllers
{
    public class UploadController : AdminControllerBase
    {
        private readonly IFileManager _fileManager;
        public IActionResult Index()
        {
            return View();
        }

        public UploadController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [Route("/Upload/Article")]
        public IActionResult UploadArticleImage(IFormFile upload)
        {
            if (upload == null)
                BadRequest();

            var imageName = _fileManager.SaveFile(upload, Directories.PostContentImage);
            return Json(new { Uploaded = true, url = Directories.GetPostContentImage(imageName) });
        }
    }
}
