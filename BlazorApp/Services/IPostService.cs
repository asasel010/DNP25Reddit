using ApiContracts;
using Entities;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<PostDTO> AddPostAsync(CreatePostDTO request); 
    public Task<Post> GetSingleAsync(int id); 
    public Task<List<Post>> GetManyAsync();
}