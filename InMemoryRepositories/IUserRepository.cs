using Entities;

namespace InMemoryRepositories;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
    Task<User> GetSingleAsync(int id); 
    IQueryable<User> GetManyAsync();
    Task DeleteAsync(int id);
}