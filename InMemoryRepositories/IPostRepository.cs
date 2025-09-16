using Entities;

namespace InMemoryRepositories
{
    public interface IPostRepository { 
        Task<Post> AddPostAsync(Post post); 
        Task UpdatePostAsync(Post post); 
        Task DeletePostAsync(int id); 
        Task<Post> GetSingleAsync(int id); 
        IQueryable<Post> GetManyAsync();
    }
}
