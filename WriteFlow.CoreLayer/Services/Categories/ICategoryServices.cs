using System;
using System.Collections.Generic;
using WriteFlow.CoreLayer.DTOs.Categories;
using WriteFlow.CoreLayer.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.CoreLayer.Services.Categories
{
    public interface ICategoryServices
    {
        OperationResult CreateCategory(CreateCategoryDto craeteDto);
        OperationResult EditCategory(EditCategoryDto editDto);
        List<CategoryDto> GetAllcategory();
        List<CategoryDto> GetChildCategories(int parentId);
        CategoryDto GetCategoryBy(int id);
        CategoryDto GetCategoryBy(string slug);
        bool IsSlugExist(string slug);
        CategoryFilterDto GetCategoryByFilter(CategoryFilterParams filterParams);
        Task DeleteCategory(int categoryId);
    }
}
