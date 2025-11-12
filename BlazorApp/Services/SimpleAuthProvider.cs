using System.Security.Claims;
using System.Text.Json;
using ApiContracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorApp.Services;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient httpClient;
    private readonly IJSRuntime jsRuntime;

    public SimpleAuthProvider(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        this.httpClient = httpClient;
        this.jsRuntime = jsRuntime;
    }

    public async Task Login(string userName, string password)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("auth/login", new LoginRequest(userName, password));
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(content);

        UserDTO userDto = JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        string serializedUser = JsonSerializer.Serialize(userDto);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serializedUser);

        NotifyAuthenticationStateChanged(Task.FromResult(CreateAuthState(userDto)));
    }

    public async Task Logout()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");

            if (string.IsNullOrEmpty(userAsJson))
                return new AuthenticationState(new());

            UserDTO userDto = JsonSerializer.Deserialize<UserDTO>(userAsJson)!;
            return CreateAuthState(userDto);
        }
        catch (InvalidOperationException)
        {
            return new AuthenticationState(new());
        }
    }

    private AuthenticationState CreateAuthState(UserDTO userDto)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.Id.ToString())
        };

        ClaimsIdentity identity = new(claims, "apiauth");
        ClaimsPrincipal claimsPrincipal = new(identity);
        return new AuthenticationState(claimsPrincipal);
    }
}
