using Microsoft.AspNetCore.Mvc;
using FileRepositories;
using ApiContracts;
using Entities;
using InMemoryRepositories;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepo;

    public PostsController(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Post>> GetMany([FromQuery] string? titleContains, [FromQuery] int? authorId)
    {
        var posts = postRepo.GetManyAsync();

        if (!string.IsNullOrEmpty(titleContains))
            posts = posts.Where(p => p.Title.Contains(titleContains, StringComparison.OrdinalIgnoreCase));

        if (authorId.HasValue)
            posts = posts.Where(p => p.UserId == authorId.Value);

        return Ok(posts.ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetSingle(int id)
    {
        try
        {
            return Ok(await postRepo.GetSingleAsync(id));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<PostDTO>> Add(CreatePostDTO dto)
    {
        var post = new Post
        {
            Title = dto.Title,
            Body = dto.Body,
            UserId = dto.AuthorId   // ✅ Make sure this line uses AuthorId from DTO
        };

        var created = await postRepo.AddPostAsync(post);

        return CreatedAtAction(nameof(GetSingle), new { id = created.Id }, new PostDTO
        {
            Id = created.Id,
            Title = created.Title,
            Body = created.Body,
            AuthorId = created.UserId
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await postRepo.DeletePostAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}

