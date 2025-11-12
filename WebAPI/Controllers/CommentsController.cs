using ApiContracts;
using Entities;
using InMemoryRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("posts/{postId:int}/comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepo;

    public CommentsController(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommentDTO>> GetMany(int postId, [FromQuery] int? userId)
    {
        var comments = commentRepo.GetManyAsync()
            .Where(c => c.PostId == postId); 

        if (userId.HasValue)
            comments = comments.Where(c => c.UserId == userId.Value);

        var result = comments.Select(c => new CommentDTO
        {
            Id = c.Id,
            Body = c.Body,
            PostId = c.PostId,
            AuthorId = c.UserId
        });

        return Ok(result.ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDTO>> GetSingle(int postId, int id)
    {
        try
        {
            var comment = await commentRepo.GetSingleAsync(id);

            if (comment.PostId != postId)
                return BadRequest("Comment does not belong to the specified post.");

            return Ok(new CommentDTO
            {
                Id = comment.Id,
                Body = comment.Body,
                PostId = comment.PostId,
                AuthorId = comment.UserId
            });
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CommentDTO>> Add(int postId, CreateCommentDTO dto)
    {
        var comment = new Comment
        {
            Body = dto.Body,
            PostId = postId,
            UserId = dto.AuthorId
        };

        var created = await commentRepo.AddCommentAsync(comment);

        var result = new CommentDTO
        {
            Id = created.Id,
            Body = created.Body,
            PostId = created.PostId,
            AuthorId = created.UserId
        };

        return CreatedAtAction(nameof(GetSingle), new { postId = postId, id = result.Id }, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int postId, int id)
    {
        try
        {
            var comment = await commentRepo.GetSingleAsync(id);
            if (comment.PostId != postId)
                return BadRequest("Comment does not belong to the specified post.");

            await commentRepo.DeleteCommentAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
