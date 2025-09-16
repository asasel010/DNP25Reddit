namespace Entities
{
    public class Post
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Body { set; get; }
        public int UserId { set; get; }
        
        public Post(int userId, string title, string body)
        {
            UserId = userId;
            Title = title;
            Body = body;
        }

        public Post(string title, string body)
        {
            Title = title;
            Body = body;
        }
    }
}
