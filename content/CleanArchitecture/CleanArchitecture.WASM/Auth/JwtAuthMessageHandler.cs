using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace CleanArchitecture.WASM.Auth;


public class JwtAuthMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public JwtAuthMessageHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsync<string>("access_token");

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}