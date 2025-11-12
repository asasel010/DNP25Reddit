using ApiContracts;
using Entities;

namespace BlazorApp.Services;

public interface ICommentService
{
    Task<List<CommentDTO>> GetCommentsForPostAsync(int postId);
    Task<CommentDTO> AddCommentAsync(int postId, CreateCommentDTO request);
}