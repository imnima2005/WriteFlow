using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WriteFlow.CoreLayer.DTOs.Posts;
using WriteFlow.CoreLayer.Services.Posts;

namespace WriteFlow.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IPostServices _postServices;

        public SearchModel(IPostServices postServices)
        {
            _postServices = postServices;
        }
        public PostFilterDto Filter { get; set; }

        public void OnGet(int pageId = 1, string categorySlug = null, string q = null)
        {
            Filter = _postServices.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 3,
                Title = q
            });
        }

        public IActionResult OnGetPagination(int pageId = 1, string categorySlug = null, string q = null)
        {
            var model = _postServices.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 3,
                Title = q
            });
            return Partial("_SearchView", model);
        }

    }
}
