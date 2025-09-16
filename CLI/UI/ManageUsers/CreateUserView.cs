using Entities;
using InMemoryRepositories;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task AddUserAsync(string name, string password)
    {
        User user = new User(name, password);
        User created = await userRepository.AddUserAsync(user);
        Console.WriteLine($"User {name} created successfully, id: {created.Id}");
    }
}