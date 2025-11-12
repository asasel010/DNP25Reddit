using ApiContracts;
using Entities;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDTO> AddUserAsync(CreateUserDTO request);
    public Task UpdateUserAsync(int id, UpdateUserDTO request);

}