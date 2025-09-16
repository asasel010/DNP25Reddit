using Entities;
using InMemoryRepositories;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task AddPostAsync(int userId, string title, string body)
    {
        Post post = new Post(userId, title, body);
        Post created = await postRepository.AddPostAsync(post);
        Console.WriteLine($"Post {title} created successfully, id: {created.Id}");
    }
}