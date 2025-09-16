namespace Entities
{
    public class Comment
    {
        public int Id { set; get; }
        public string Body { set; get; }
        public int UserId { set; get; }
        public int PostId { set; get; }

        public Comment(int userId, int postId, string body)
        {
            UserId = userId;
            PostId = postId;
            Body = body;
        }

        public Comment(string body)
        {
            Body = body;
        }
        
    }
}
