using Entities;
using InMemoryRepositories;

namespace RepositoryContracts
{
    public class UserInMemoryRepository : IUserRepository
    {
        public List<User> users;

        public Task<User> AddUserAsync(User user)
        {
            user.Id = users.Any() ? users.Max(p => p.Id) + 1 : 1;
            users.Add(user);
            return Task.FromResult(user);
        }
        
        public Task<User> GetSingleAsync(int id)
        { 
            User? userToFind = users.SingleOrDefault(p => p.Id == id);
            if (userToFind is null)
            {
                throw new InvalidOperationException($"user with ID '{id}' not found");
            }
            
            return Task.FromResult(userToFind);
        }

        public IQueryable<User> GetManyAsync() { 
            return users.AsQueryable(); 
        }
    }
}
