using ApiContracts;
using InMemoryRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Username and password are required.");

        var user = await _userRepository.GetByUsernameAsync(request.UserName);

        if (user == null)
            return Unauthorized("User not found.");

        if (user.Password != request.Password)
            return Unauthorized("Incorrect password.");

        var userDto = new UserDTO
        {
            Id = user.Id,
            Username = user.Username
        };

        return Ok(userDto);
    }
}