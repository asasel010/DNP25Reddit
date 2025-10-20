namespace ApiContracts;

public class CreateCommentDTO
{
    public required string Body { get; set; }
    public required int AuthorId { get; set; }
    public required int PostId { get; set; }
}