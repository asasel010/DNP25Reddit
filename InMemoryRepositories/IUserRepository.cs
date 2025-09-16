using Entities;

namespace InMemoryRepositories;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user); 
    Task<User> GetSingleAsync(int id); 
    IQueryable<User> GetManyAsync();
}