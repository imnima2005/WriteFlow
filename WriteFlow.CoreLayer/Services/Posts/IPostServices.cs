using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.Utilities;
using WriteFlow.CoreLayer.DTOs.Posts;

namespace WriteFlow.CoreLayer.Services.Posts
{
    public interface IPostServices
    {
        OperationResult CreatePost(CreatePostDto dataObject);
        OperationResult EditPost(EditPostDto dataObject);
        List<PostDto> GetAllPost();
        PostDto GetPostBySlug(string slug);
        bool IsSlugExist(string slug);
        PostFilterDto GetPostByFilter(PostFilterParams filterParams);
        PostDto GetPostById(int id);
        List<PostDto> GetRelatedPost(int categoryId);
        List<PostDto> GetPopularPost();
        void visitCount(int postId);
        OperationResult DeletePost(int postId);
       
    }
}
