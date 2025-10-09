using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace TodoListApp.WebApp.Handler;

public class AuthMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public AuthMessageHandler(ILocalStorageService localStorage)
    {
        this._localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await this._localStorage.GetItemAsync<string>("authToken");

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
