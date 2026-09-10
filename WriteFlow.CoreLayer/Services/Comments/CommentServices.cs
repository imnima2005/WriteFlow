using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.DTOs.Comments;
using WriteFlow.CoreLayer.Utilities;
using WriteFlow.DataLayer.Context;
using WriteFlow.DataLayer.Entities;

namespace WriteFlow.CoreLayer.Services.Comments
{
    public class CommentServices : ICommentServices
    {
        private readonly WriteFlowContext _context;

        public CommentServices(WriteFlowContext context)
        {
            _context = context;
        }

        public OperationResult CreateComment(CreateCommentDto command)
        {
            var comment = new PostComment()
            {
                PostId = command.PostId,
                TextBody = command.Text,
                UserId = command.UserId
            };
            _context.Add(comment);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<CommentDto> GetPostComments(int postId)
        {
            return _context.PostComment
                .Include(c => c.User)
                .Where(c => c.PostId == postId)
                .Select(comment => new CommentDto()
                {
                    PostId = comment.PostId,
                    Text = comment.TextBody,
                    UserFullName = comment.User.FullName,
                    CommentId = comment.Id,
                    CreationDate = comment.CreatedAt
                }).ToList();
        }
    }
}
