using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.Services.Categories;
using WriteFlow.Areas.Admin.Models.Category;
using WriteFlow.CoreLayer.Utilities;
using WriteFlow.CoreLayer.DTOs.Categories;
using Microsoft.AspNetCore.Authorization;

namespace WriteFlow.Areas.Admin.Controllers
{
    public class CategoryController : AdminControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public IActionResult Index(int pageId = 1, string title = "", string slug = "")
        {
            var param = new CategoryFilterParams()
            {
                Slug = slug,
                Title = title,
                PageId = pageId,
                Take = 1
            };

            var model = _categoryServices.GetCategoryByFilter(param);

            return View(model);
        }


        [Route("/Admin/category/add/{parentId?}")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost("/Admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId, CreateCategoryViewModel createViewModel)
        {
            createViewModel.ParentId = parentId;

            var result = _categoryServices.CreateCategory(new CoreLayer.DTOs.Categories.CreateCategoryDto()
            {
                Title = createViewModel.Title,
                MetaDescription = createViewModel.MetaDescription,
                Slug = createViewModel.Slug,
                MetaTag = createViewModel.MetaTag,
                ParentId = createViewModel.ParentId
            });

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }

        
        [Route("/admin/category/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var selectedCategory = _categoryServices.GetCategoryBy(id);
            if (selectedCategory == null)
                return RedirectToAction("Index");

            var model = new EditCategoryViewModel()
            {
                Id = selectedCategory.Id,
                Slug = selectedCategory.Slug,
                Title = selectedCategory.Title,
                MetaDescription = selectedCategory.MetaDescription,
                MetaTag = selectedCategory.MetaTag
            };
            return View(model);
        }

        
        [HttpPost]
        [Route("/admin/category/edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategoryViewModel editModel)
        {
            var result = _categoryServices.EditCategory(new CoreLayer.DTOs.Categories.EditCategoryDto()
            {
                Id = editModel.Id,
                Slug = editModel.Slug,
                Title = editModel.Title,
                MetaTag = editModel.MetaTag,
                MetaDescription = editModel.MetaDescription,
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(editModel);
            }

            return RedirectToAction("Index");
        }

        public IActionResult GetChildCategories(int parentId)
        {
            var category = _categoryServices.GetChildCategories(parentId);
            return new JsonResult(category);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _categoryServices.DeleteCategory(categoryId);
            return RedirectToAction("Index");
        }

    }
}
