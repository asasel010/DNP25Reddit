using ApiContracts;
using Microsoft.AspNetCore.Mvc;
using FileRepositories;
using Entities;
using InMemoryRepositories;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetMany([FromQuery] string? usernameContains)
    {
        var users = userRepo.GetManyAsync();
        if (!string.IsNullOrEmpty(usernameContains))
        {
            users = users.Where(u => u.Username.Contains(usernameContains, StringComparison.OrdinalIgnoreCase));
        }
        return Ok(users.ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetSingle(int id)
    {
        try
        {
            return Ok(await userRepo.GetSingleAsync(id));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody] User user)
    {
        var created = await userRepo.AddUserAsync(user);
        return Created($"/users/{created.Id}", created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await userRepo.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}

