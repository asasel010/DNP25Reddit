namespace Entities
{
    public class User
    {
        public int Id { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
