using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace CleanArchitecture.WASM.Auth;


public class AuthService
{
    private readonly HttpClient _http;
    private readonly JwtAuthenticationStateProvider _authState;

    public AuthService(
        HttpClient http,
        AuthenticationStateProvider authState)
    {
        _http = http;
        _authState = (JwtAuthenticationStateProvider)authState;
    }

    public async Task LoginAsync()
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", new { });

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        await _authState.MarkUserAsAuthenticated(result);
    }

    public async Task LogoutAsync()
    {
        await _authState.MarkUserAsLoggedOut();
    }
}

public record LoginResponse(string Token, string UserName);