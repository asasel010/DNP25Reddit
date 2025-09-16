using Entities;
using InMemoryRepositories;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task<Post> SinglePostAsync(int postId)
    {
        Post post = await postRepository.GetSingleAsync(postId);;
        return post;

    }
}