using Entities;
using InMemoryRepositories;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        Comment? found = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);

        if (found == null)
            throw new Exception($"Comment with id {id} not found");

        return found;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return ctx.Comments.AsQueryable();
    }

    public async Task DeleteCommentAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
        if (existing == null)
            throw new Exception($"Comment with id {id} not found");

        ctx.Comments.Remove(existing);
        await ctx.SaveChangesAsync();
    }
}