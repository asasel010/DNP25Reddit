using Entities;
using InMemoryRepositories;

namespace CLI.UI.ManageComments;

public class ListCommentsView
{
    private readonly ICommentRepository commentRepository;

    public ListCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task<IQueryable<Comment>> ListCommentsAsync()
    {
        IQueryable<Comment> comments = commentRepository.GetManyAsync();
        return comments;
    }
}