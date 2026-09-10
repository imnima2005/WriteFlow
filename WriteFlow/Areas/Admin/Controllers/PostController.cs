using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteFlow.Areas.Admin.Models.Post;
using WriteFlow.CoreLayer.DTOs.Posts;
using WriteFlow.CoreLayer.Services.Posts;
using WriteFlow.CoreLayer.Utilities;

namespace WriteFlow.Areas.Admin.Controllers
{
    public class PostController : AdminControllerBase
    {
        private readonly IPostServices _postServices;

        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }

        [HttpGet]
        public IActionResult Index(int pageId = 1, string title = "", string categorySlug = "")
        {
            var param = new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Title = title,
                Take = 5
            };

            var model = _postServices.GetPostByFilter(param);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            
            var result = _postServices.CreatePost(new CreatePostDto() { 
                CategoryId = viewModel.CategoryId,
                Description = viewModel.Description,
                ShortDescription = viewModel.ShortDescription,
                ImageFile = viewModel.ImageFile,
                Slug = viewModel.Slug,
                SubCategoryId = viewModel.SubCategoryId,
                Title = viewModel.Title,
                UserId = User.GetUserId(),
                IsSpecial = viewModel.IsSpecial
            
            });

            if (result.Status != CoreLayer.Utilities.OperationResultStatus.Success)
                return View(viewModel);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _postServices.GetPostById(id);
            if (post == null)
                return RedirectToAction("Index");

            var model = new EditPostViewModel() 
            {
                CategoryId = post.CategoryId,
                Description = post.Description,
                ShortDescription = post.ShortDescription,
                Slug = post.Slug,
                SubCategoryId = post.SubCategoryId,
                Title = post.Title,
                IsSpecial = post.IsSpecial
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditPostViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var result = _postServices.EditPost(new EditPostDto()
            {
                CategoryId = viewModel.CategoryId,
                Description = viewModel.Description,
                ShortDescription = viewModel.ShortDescription,
                Title = viewModel.Title,
                Slug = viewModel.Slug,
                ImageFile = viewModel.ImageFile,
                SubCategoryId = viewModel.SubCategoryId == 0 ? null : viewModel.SubCategoryId,
                PostId = viewModel.Id,
                IsSpecial = viewModel.IsSpecial
            });

            if (result.Status != CoreLayer.Utilities.OperationResultStatus.Success)
                return View(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _postServices.DeletePost(id);
            if (result.Status == OperationResultStatus.NotFound)
                return NotFound();
            return RedirectToAction("Index");
        }
    }



}
