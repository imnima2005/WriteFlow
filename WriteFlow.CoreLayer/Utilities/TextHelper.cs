using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.CoreLayer.Utilities
{
    public static class TextHelper
    {
        public static string ToSlug(this string value)
        {
            return value.Trim().ToLower()
                .Replace("~", "")
                .Replace("!", "")
                .Replace("@", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("%", "")
                .Replace("^", "")
                .Replace("&", "")
                .Replace("*", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("+", "")
                .Replace("=", "")
                .Replace(@"\", "")
                .Replace("/", "")
                .Replace(" ", "-");
        }
    }
}
