using Entities;

namespace InMemoryRepositories;

public interface ICommentRepository
{
    Task<Comment> AddCommentAsync(Comment comment); 
    Task<Comment> GetSingleAsync(int id); 
    IQueryable<Comment> GetManyAsync();
}