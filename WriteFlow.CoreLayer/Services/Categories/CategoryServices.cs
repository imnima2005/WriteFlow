using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.DataLayer.Context;
using WriteFlow.CoreLayer.DTOs.Categories;
using WriteFlow.CoreLayer.Utilities;
using WriteFlow.DataLayer.Entities;
using WriteFlow.CoreLayer.Mappers;
using Microsoft.EntityFrameworkCore;

namespace WriteFlow.CoreLayer.Services.Categories
{
    public class CategoryServices : ICategoryServices
    {
        private readonly WriteFlowContext _Context;

        public CategoryServices(WriteFlowContext context)
        {
            _Context = context;
        }

        public OperationResult CreateCategory(CreateCategoryDto createDto)
        {
            if (IsSlugExist(createDto.Slug))
                return OperationResult.Error("عبارت وارد شده تکراری است.");

            var category = new Category()
            {
                Title = createDto.Title,
                MetaTag = createDto.MetaTag,
                MetaDescription = createDto.MetaDescription,
                Slug = createDto.Slug.ToSlug(),
                ParentId = createDto.ParentId
            };

            _Context.Categories.Add(category);
            _Context.SaveChanges();
            return OperationResult.Success();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var categories = await _Context.Categories
                .Include(c => c.SubCategories)
                .ToListAsync();

            var target = categories.FirstOrDefault(c => c.Id == categoryId);
            if (target != null)
            {
                MarkAsDeletedRecursive(target);
                await _Context.SaveChangesAsync();
            }
        }

        public void MarkAsDeletedRecursive(Category category)
        {
            category.IsDelete = true;
            if (category.SubCategories != null)
            {
                foreach (var child in category.SubCategories)
                {
                    MarkAsDeletedRecursive(child);
                }
            }
        }

        public OperationResult EditCategory (EditCategoryDto editDto)
        {
            var category = _Context.Categories.FirstOrDefault(c => c.Id == editDto.Id);

            if (category == null)
                return OperationResult.NotFound();
            if (editDto.Slug.ToSlug() != category.Slug)
                if (IsSlugExist(editDto.Slug.ToSlug()))
                    return OperationResult.Error("عبارت وارد شده تکراری است.");

            category.Title = editDto.Title;
            category.Slug = editDto.Slug.ToSlug();
            category.MetaTag = editDto.MetaTag;
            category.MetaDescription = editDto.MetaDescription;

            _Context.SaveChanges();
            return OperationResult.Success();
        }

        public List<CategoryDto> GetAllcategory()
        {
            return _Context.Categories.Select(c => CategoryMapper.Map(c)).ToList();
        }

        public CategoryDto GetCategoryBy(int id)
        {
            var category = _Context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return null;

            return CategoryMapper.Map(category);
        }

        public CategoryDto GetCategoryBy(string slug)
        {
            var category = _Context.Categories.FirstOrDefault(c => c.Slug == slug);
            if (category == null)
                return null;

            return CategoryMapper.Map(category);
        }

        public CategoryFilterDto GetCategoryByFilter(CategoryFilterParams filterParams)
        {
            var result = _Context.Categories
                .Where(c=>c.ParentId == null)
                .OrderByDescending(d => d.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.Slug))
                result = result.Where(r => r.Slug.Contains(filterParams.Slug));

            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                result = result.Where(r => r.Title.Contains(filterParams.Title));

            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var model = new CategoryFilterDto()
            {
                AllCategory = _Context.Categories.Select(c => CategoryMapper.Map(c)).ToList(),
                Categories = result.Skip(skip).Take(filterParams.Take)
                        .Select(Category => CategoryMapper.Map(Category)).ToList(),
                FilterParams = filterParams
            };
            model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
            return model;
        }

        public List<CategoryDto> GetChildCategories(int parentId)
        {
            return _Context.Categories.Where(r => r.ParentId == parentId)
                .Select(category => CategoryMapper.Map(category)).ToList();
        }

        public bool IsSlugExist(string slug)
        {
            return _Context.Categories.Any(c => c.Slug == slug.ToSlug());
        }
    }
}
