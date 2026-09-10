using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.CoreLayer.DTOs.Posts
{
    public class EditPostDto
    {
        public IFormFile ImageFile { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int VisitCount { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int PostId { get; set; }
        public bool IsSpecial { get; set; }
    }
}
