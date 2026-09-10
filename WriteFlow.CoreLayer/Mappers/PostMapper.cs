using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.DataLayer.Entities;
using WriteFlow.CoreLayer.DTOs.Posts;
using WriteFlow.CoreLayer.DTOs.Categories;

namespace WriteFlow.CoreLayer.Mappers
{
    public class PostMapper
    {
        public static Post MapCreateDtoToPost(CreatePostDto dto)
        {
            return new Post()
            {
                Title = dto.Title,
                Slug = dto.Slug,
                Description = dto.Description,
                ShortDescription = dto.ShortDescription,
                CategoryId = dto.CategoryId,
                UserId = dto.UserId,
                SubCategoryId = dto.SubCategoryId
            };
        }

        public static Post MapEditDtoToPost(EditPostDto dto)
        {
            return new Post()
            {
                Title = dto.Title,
                Slug = dto.Slug,
                Description = dto.Description,
                ShortDescription = dto.ShortDescription
            };
        }

        public static PostDto MapPostToDto (Post post)
        {
            return new PostDto()
            {
                Title = post.Title,
                Slug = post.Slug,
                Description = post.Description,
                ShortDescription = post.ShortDescription,
                VisitCount = post.VisitCount,
                CategoryId = post.CategoryId,
                SubCategoryId = post.SubCategoryId,
                ImageName = post.ImageName,
                Category = CategoryMapper.Map(post.Category),
                CreationDate = post.CreatedAt,
                UserId = post.UserId,
                PostId = post.Id,
                SubCategory = post.SubCategoryId == null ? null : CategoryMapper.Map(post.SubCategory),
                UserFullName = post.User?.FullName,
                IsSpecial = post.IsSpecial
                
            };
        }

        public static Post EditPost(EditPostDto editDto, Post post)
        {
            post.Description = editDto.Description;
            post.ShortDescription = editDto.ShortDescription;
            post.Title = editDto.Title;
            post.CategoryId = editDto.CategoryId;
            post.Slug = editDto.Slug;
            post.SubCategoryId = editDto.SubCategoryId;
            post.IsSpecial = editDto.IsSpecial;
            return post;
        }
    }
}


/* Category = post.Category != null ? new CategoryDto
{
    Id = post.Category.Id,
    Title = post.Category.Title,
    Slug = post.Category.Slug
} : null,
*/