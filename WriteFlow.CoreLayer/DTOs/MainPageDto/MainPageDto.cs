using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.DTOs.Posts;

namespace WriteFlow.CoreLayer.DTOs.MainPageDto
{
    public class MainPageDto
    {
        public List<PostDto> LastPosts { get; set; }
        public List<PostDto> SpecialPosts { get; set; }
        public List<MainPageCategoryDto> Categories { get; set; }
    }

    public class MainPageCategoryDto
    {
        public bool IsMainCategory { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public int PostChild { get; set; }
    }

    public class SiteStatsDto
    {
        public int UserCount { get; set; }
        public int PostCount { get; set; }
        public int CategoryCount { get; set; }
    }
}
