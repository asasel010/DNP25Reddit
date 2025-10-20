namespace ApiContracts;

public class CommentDTO
{
    public int Id { get; set; }
    public string Body { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public int PostId { get; set; }
}