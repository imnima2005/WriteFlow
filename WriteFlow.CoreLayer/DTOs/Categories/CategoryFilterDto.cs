using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.Utilities;

namespace WriteFlow.CoreLayer.DTOs.Categories
{
    public class CategoryFilterDto: BasePagination
    {
        public List<CategoryDto> AllCategory { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public CategoryFilterParams FilterParams { get; set; }
    }

    public class CategoryFilterParams
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int Take { get; set; }
    }
}
