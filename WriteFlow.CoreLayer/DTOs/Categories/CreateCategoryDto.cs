using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.CoreLayer.DTOs.Categories
{
    public class CreateCategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }
        public int? ParentId { get; set; }
    }
}
