using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WriteFlow.CoreLayer.Services.Posts;
using WriteFlow.CoreLayer.DTOs.Posts;
using WriteFlow.CoreLayer.Services.MainPage;
using WriteFlow.CoreLayer.DTOs.MainPageDto;

namespace WriteFlow.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPostServices _postServices;
        private readonly IMainPageServices _mainPageServices;

        public IndexModel(IPostServices postServices, IMainPageServices mainPageServices)
        {
            _postServices = postServices;
            _mainPageServices = mainPageServices;
        }

        public List<PostDto> Posts { get; set; }
        public MainPageDto MainPageData { get; set; }
        
        public void OnGet()
        {
            MainPageData = _mainPageServices.GetData();
        }

        public IActionResult OnGetLatestPosts(string categorySlug)
        {
            var filterDto = _postServices.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = 1,
                Take = 6
            });
            return Partial("_LatestPosts", filterDto.Posts);
        }

        public IActionResult OnGetPopularPost()
        {
            return Partial("_PopularPosts", _postServices.GetPopularPost());
        }
    }
}
