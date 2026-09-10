using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.Utilities;
using WriteFlow.CoreLayer.DTOs.Posts;
using WriteFlow.DataLayer.Context;
using WriteFlow.CoreLayer.Mappers;
using WriteFlow.CoreLayer.Services.FileManager;
using Microsoft.EntityFrameworkCore;

namespace WriteFlow.CoreLayer.Services.Posts
{
    public class PostServices : IPostServices
    {
        private readonly WriteFlowContext _context;
        private readonly IFileManager _fileManager;
        public PostServices (WriteFlowContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        OperationResult IPostServices.CreatePost(CreatePostDto dataObject)
        {
            if (dataObject.ImageFile == null)
                return OperationResult.Error();
            
            var post = PostMapper.MapCreateDtoToPost(dataObject);
            post.ImageName = _fileManager.SaveImage(dataObject.ImageFile, Directories.PostImage);
            _context.Posts.Add(post);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        OperationResult IPostServices.EditPost(EditPostDto dataObject)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == dataObject.PostId);
            var oldImage = post.ImageName;

            if (post == null)
                return OperationResult.NotFound();

            PostMapper.EditPost(dataObject, post);
            if (dataObject.ImageFile != null)
                post.ImageName = _fileManager.SaveImage(dataObject.ImageFile, Directories.PostImage);

            _context.SaveChanges();

            if (dataObject.ImageFile != null)
                _fileManager.DeleteFile(oldImage, Directories.PostImage);

            return OperationResult.Success();
        }

        List<PostDto> IPostServices.GetAllPost()
        {
            return _context.Posts.Select(p => PostMapper.MapPostToDto(p)).ToList();
        }

        PostDto IPostServices.GetPostBySlug(string slug)
        {
            var post = _context.Posts
                .Include(c=>c.Category)
                .Include(c=>c.SubCategory)
                .Include(c=>c.User)
                .FirstOrDefault(p => p.Slug == slug);

            if (post == null)
                return null;

            return PostMapper.MapPostToDto(post);
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Posts.Any(p => p.Slug == slug);
        }

        public PostFilterDto GetPostByFilter(PostFilterParams filterParams)
        {
            var result = _context.Posts
                .Include(d => d.User)
                .Include(d => d.Category)
                .Include(d => d.SubCategory)
                .OrderByDescending(d => d.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
                result = result.Where(r => r.Category.Slug.Contains(filterParams.CategorySlug) || r.SubCategory.Slug.Contains(filterParams.CategorySlug));

            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                result = result.Where(r => r.Title.Contains(filterParams.Title));

            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var postCount = result.Count();
            var model = new PostFilterDto()
            {
                Posts = result.Skip(skip).Take(filterParams.Take)
                        .Select(post => PostMapper.MapPostToDto(post)).ToList(),
                FilterParams = filterParams
            };
            model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
            return model;
               
        }

        public PostDto GetPostById(int id)
        {
            var post = _context.Posts
                .Include(c=>c.SubCategory)
                .Include(c=>c.Category)
                .FirstOrDefault(p => p.Id == id);
            return PostMapper.MapPostToDto(post);
        }

        public List<PostDto> GetRelatedPost(int categoryId)
        {
            return _context.Posts
                .Where(r => r.CategoryId == categoryId || r.SubCategoryId == categoryId)
                .OrderByDescending(d => d.CreatedAt)
                .Take(3).Select(post => PostMapper.MapPostToDto(post)).ToList();
        }

        public List<PostDto> GetPopularPost()
        {
            return _context.Posts
                .Include(c => c.User)
                .OrderByDescending(v => v.VisitCount)
                .Take(5).Select(post => PostMapper.MapPostToDto(post)).ToList();
        }

        public void visitCount(int postId)
        {
            var post = _context.Posts.First(p => p.Id == postId);
            post.VisitCount += 1;
            _context.SaveChanges();
        }

        public OperationResult DeletePost(int postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
                return OperationResult.NotFound("مقاله مورد نظر یافت نشد.");

            post.IsDelete = true;
            _context.SaveChanges();

            return OperationResult.Success();
        }
    }
}
