using Entities;
using InMemoryRepositories;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task AddCommentAsync(int userId, int postId, string body)
    {
        Comment comment = new Comment(userId, postId, body);
        Comment created = await commentRepository.AddCommentAsync(comment);
        Console.WriteLine($"Comment added successfully, comment id: {created.Id}");
    }
}