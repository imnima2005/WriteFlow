using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.DTOs.Comments;
using WriteFlow.CoreLayer.Utilities;

namespace WriteFlow.CoreLayer.Services.Comments
{
    public interface ICommentServices
    {
        OperationResult CreateComment(CreateCommentDto command);
        List<CommentDto> GetPostComments(int postId);
    }
}
