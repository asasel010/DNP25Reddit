using Entities;
using InMemoryRepositories;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository 
{ 
    private readonly AppContext ctx;

    public EfcPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    
    public async Task<Post> AddPostAsync(Post post)
    {
        await ctx.Posts.AddAsync(post); 
        await ctx.SaveChangesAsync(); 
        return post;
    }

    public async Task UpdatePostAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p => p.Id == post.Id)))
        {
            throw new Exception("Post with id {post.Id} not found");
        }

        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        ctx.Posts.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public Task<Post?> GetSingleAsync(int id)
    {
        return ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
    }

    public IQueryable<Post> GetManyAsync()
    {
        return ctx.Posts.AsQueryable();
    }
}