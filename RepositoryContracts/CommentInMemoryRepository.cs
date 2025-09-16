using Entities;
using InMemoryRepositories;

namespace RepositoryContracts
{
    public class CommentInMemoryRepository : ICommentRepository
    {
        public List<Comment> comments;

        public Task<Comment> AddCommentAsync(Comment comment)
        {
            comment.Id = comments.Any() ? comments.Max(p => p.Id) + 1 : 1;
            comments.Add(comment);
            return Task.FromResult(comment);
        }
        
        public Task<Comment> GetSingleAsync(int id)
        { 
            Comment? postToFind = comments.SingleOrDefault(p => p.Id == id);
            if (postToFind is null)
            {
                throw new InvalidOperationException($"Post with ID '{id}' not found");
            }
            
            return Task.FromResult(postToFind);
        }
        
        /*public IQueryable<Comment> GetPostCommentsAsync(int id)
        { 
            Comment? postToFind = comments.SingleOrDefault(p => p.Id == id);
            if (postToFind is null)
            {
                throw new InvalidOperationException($"Post with ID '{id}' not found");
            }
            
            return comments.AsQueryable(); 
            return Task.FromResult(postToFind);
        }*/


        public IQueryable<Comment> GetManyAsync() { 
            return comments.AsQueryable(); 
        }
    }
}
