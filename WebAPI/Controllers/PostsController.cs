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
    public async Task<ActionResult<Post>> Add([FromBody] Post post)
    {
        var created = await postRepo.AddPostAsync(post);
        return Created($"/posts/{created.Id}", created);
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

