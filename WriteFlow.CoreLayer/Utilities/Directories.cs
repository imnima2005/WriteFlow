using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.CoreLayer.Utilities
{
    public class Directories
    {
        public const string PostImage = "wwwroot/images/posts";
        public const string PostContentImage = "wwwroot/images/posts/content";
        public static string GetPostImage(string imageName) => $"{PostImage.Replace("wwwroot", "")}/{imageName}".Replace("\\", "/");
        public static string GetPostContentImage(string imageName) => $"{PostContentImage.Replace("wwwroot", "")}/{imageName}";
    }
}

// \images\posts\8c895d17-2575-46ab-b06d-fb93ac45e48ejupiter-moons-of-jupiter-solar-system-planet-outer-space-1920x1200-8762.jpg