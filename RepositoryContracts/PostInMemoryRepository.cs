using Entities;
using InMemoryRepositories;

namespace RepositoryContracts
{
    public class PostInMemoryRepository : IPostRepository
    {
        public List<Post> posts;

        public PostInMemoryRepository()
        {
            posts = new List<Post>
            {
                new Post("reditgold pls","mom sky much sad +10000 karma plz"){Id = 1,UserId = 1},
                new Post("how to be more annoying","hello i drive bmw im very loud and obnoxious, how to be even more annoying, i already removed my muffler XD, what else could I do?"){Id = 2,UserId = 2}
            };
        }

        public Task<Post> AddPostAsync(Post post)
        {
            post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
            posts.Add(post);
            return Task.FromResult(post);
        }

        public Task UpdatePostAsync(Post post)
        {
            Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
            if (existingPost is null)
            {
                throw new InvalidOperationException($"Post with ID '{post.Id}' not found");
            }
            posts.Remove(existingPost);

            posts.Add(post);
            return Task.CompletedTask;
        }

        public Task DeletePostAsync(int id)
        {
            Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
            if (postToRemove is null)
            {
                throw new InvalidOperationException($"Post with ID '{id}' not found");
            }
            posts.Remove(postToRemove);
            return Task.CompletedTask;
        }

        public Task<Post> GetSingleAsync(int id)
        { 
            Post? postToFind = posts.SingleOrDefault(p => p.Id == id);
            if (postToFind is null)
            {
                throw new InvalidOperationException($"Post with ID '{id}' not found");
            }
            
            return Task.FromResult(postToFind);
        }

        public IQueryable<Post> GetManyAsync() { 
            return posts.AsQueryable(); 
        }
    }
}
