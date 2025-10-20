using Entities;
using InMemoryRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepo;

    public CommentsController(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Comment>> GetMany([FromQuery] int? userId, [FromQuery] int? postId)
    {
        var comments = commentRepo.GetManyAsync();

        if (userId.HasValue)
            comments = comments.Where(c => c.UserId == userId.Value);

        if (postId.HasValue)
            comments = comments.Where(c => c.PostId == postId.Value);

        return Ok(comments.ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Comment>> GetSingle(int id)
    {
        try
        {
            return Ok(await commentRepo.GetSingleAsync(id));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> Add([FromBody] Comment comment)
    {
        var created = await commentRepo.AddCommentAsync(comment);
        return Created($"/comments/{created.Id}", created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await commentRepo.DeleteCommentAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
