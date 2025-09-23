using Entities;
using InMemoryRepositories;

namespace RepositoryContracts
{
    public class CommentInMemoryRepository : ICommentRepository
    {
        public List<Comment> comments;

        public CommentInMemoryRepository()
        {
            comments = new List<Comment>
            {
                new Comment("have you heard of the finnish hospital?"){Id = 1, UserId = 3, PostId = 2},
                new Comment("wholesome 100 keanuchunger deluxe"){Id = 2, UserId = 3, PostId = 1}
            };
        }

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
        public IQueryable<Comment> GetManyAsync() { 
            return comments.AsQueryable(); 
        }
    }
}
