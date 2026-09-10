using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.DTOs.Categories;

namespace WriteFlow.CoreLayer.DTOs.Posts
{
    public class PostDto
    {
        public string UserFullName { get; set; }
        public bool IsSpecial { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int VisitCount { get; set; }
        public string ImageName { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public CategoryDto SubCategory{ get; set; }
        public DateTime CreationDate { get; set; }
    }
}
