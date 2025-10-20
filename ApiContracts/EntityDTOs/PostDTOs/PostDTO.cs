namespace ApiContracts;

public class PostDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int AuthorId { get; set; }
}