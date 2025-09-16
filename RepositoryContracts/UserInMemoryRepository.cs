using Entities;
using InMemoryRepositories;

namespace RepositoryContracts
{
    public class UserInMemoryRepository : IUserRepository
    {
        public List<User> users;

        public UserInMemoryRepository()
        {
            users = new List<User>
            {
                new User("chungus", "kappa") { Id = 1 },
                new User("BigBoss", "bmw") { Id = 2 }, 
                new User("anonguy", "87S#4XD9A*$@&X") { Id = 3 }
            };
        }

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
