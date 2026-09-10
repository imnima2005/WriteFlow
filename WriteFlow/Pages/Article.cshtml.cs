using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WriteFlow.CoreLayer.DTOs.Comments;
using WriteFlow.CoreLayer.DTOs.Posts;
using WriteFlow.CoreLayer.Services.Comments;
using WriteFlow.CoreLayer.Services.Posts;
using WriteFlow.CoreLayer.Utilities;

namespace WriteFlow.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IPostServices _postServices;
        private readonly ICommentServices _commentServices;

        public ArticleModel(IPostServices postServices, ICommentServices commentServices)
        {
            _postServices = postServices;
            _commentServices = commentServices;
        }
        public PostDto Post { get; set; }

        [Required]
        [BindProperty]
        public string TextComment { get; set; }

        [BindProperty]
        public int PostId { get; set; }

        public List<CommentDto> Comments { get; set; }
        public List<PostDto> RelatedPost { get; set; }

        public IActionResult OnGet(string slug)
        {
            Post = _postServices.GetPostBySlug(slug);

            if (Post == null)
                return NotFound();

            Comments = _commentServices.GetPostComments(Post.PostId);
            RelatedPost = _postServices.GetRelatedPost(Post.SubCategoryId ?? Post.CategoryId);
            _postServices.visitCount(Post.PostId);
            return Page();
        }

        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("Post", new { slug });

            if (!ModelState.IsValid)
            {
                Post = _postServices.GetPostBySlug(slug);
                Comments = _commentServices.GetPostComments(Post.PostId);
                RelatedPost = _postServices.GetRelatedPost(Post.SubCategoryId ?? Post.CategoryId);
                return Page();
            }

            _commentServices.CreateComment(new CreateCommentDto()
            {
                PostId = PostId,
                Text = TextComment,
                UserId = User.GetUserId()
            });

            return RedirectToPage("Article", new { slug });
        }
    }
}
