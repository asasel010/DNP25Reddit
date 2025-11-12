using System.Text.Json;
using System.Net.Http.Json;
using ApiContracts;

namespace BlazorApp.Services
{
    public class HttpCommentService : ICommentService
    {
        private readonly HttpClient client;

        public HttpCommentService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<CommentDTO>> GetCommentsForPostAsync(int postId)
        {
            HttpResponseMessage httpResponse = await client.GetAsync($"posts/{postId}/comments");
            string response = await httpResponse.Content.ReadAsStringAsync();
            
            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception($"HTTP {(int)httpResponse.StatusCode}: {response}");

            return JsonSerializer.Deserialize<List<CommentDTO>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }


        public async Task<CommentDTO> AddCommentAsync(int postId, CreateCommentDTO request)
        {
            HttpResponseMessage httpResponse = await client.PostAsJsonAsync($"posts/{request.PostId}/comments", request);
            string response = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception(response);

            return JsonSerializer.Deserialize<CommentDTO>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }
    }
}