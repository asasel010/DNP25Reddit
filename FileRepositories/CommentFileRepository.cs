using System.Text.Json;
using Entities;
using InMemoryRepositories;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        Comment? commentToFind = comments.SingleOrDefault(c => c.Id == id);

        if (commentToFind is null)
            throw new InvalidOperationException($"Comment with ID '{id}' not found");

        return commentToFind;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }

    public async Task DeleteCommentAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? commentToDelete = comments.SingleOrDefault(c => c.Id == id);
        if (commentToDelete is null)
            throw new InvalidOperationException($"Comment with ID '{id}' not found");

        comments.Remove(commentToDelete);
        commentsAsJson = JsonSerializer.Serialize(comments, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
}
