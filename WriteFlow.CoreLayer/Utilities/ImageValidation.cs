using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.CoreLayer.Utilities
{
    public class ImageValidation
    {
        public static bool Validate(string imageName)
        {
            var extention = Path.GetExtension(imageName);
            if (extention == null)
                return false;
            return extention.ToLower() == ".png" || extention.ToLower() == ".jpg";
        }
    }
}
