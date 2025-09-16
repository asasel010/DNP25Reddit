using Entities;
using InMemoryRepositories;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task<IQueryable<Post>> ListPostsAsync()
    {
        IQueryable<Post> posts = postRepository.GetManyAsync();
        return posts;
    }
}