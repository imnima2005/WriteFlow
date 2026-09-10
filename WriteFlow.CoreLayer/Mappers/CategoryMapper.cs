using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.DTOs.Categories;
using WriteFlow.DataLayer.Entities;

namespace WriteFlow.CoreLayer.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto Map (Category category)
        {
            if (category == null)
                return null;

            return new CategoryDto()
            {
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Title = category.Title,
                Slug = category.Slug,
                Id = category.Id,
                ParentId = category.ParentId
            };
        }
    }
}
