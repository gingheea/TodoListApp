#pragma warning disable CA1822
namespace TodoListApp.WebApp.Service
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text.Json;
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
        {
            this._localStorage = localStorage;
            this._httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await this._localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            this._httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var claims = this.ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token)
        {
            this._httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var claims = this.ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);

            this.NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public void NotifyUserLogout()
        {
            // ❌ При логауті прибираємо заголовок
            _httpClient.DefaultRequestHeaders.Authorization = null;

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = this.ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonBytes)!;

            var claims = new List<Claim>();
            foreach (var kvp in keyValuePairs)
            {
                // standard claim fields mapping
                if (kvp.Key == "role")
                {
                    if (kvp.Value.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var r in kvp.Value.EnumerateArray())
                        {
                            claims.Add(new Claim(ClaimTypes.Role, r.GetString()!));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, kvp.Value.GetString()!));
                    }
                }
                else if (kvp.Key == "sub" || kvp.Key == "nameid" || kvp.Key == "uid" || kvp.Key == "id")
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, kvp.Value.GetString()!));
                }
                else
                {
                    claims.Add(new Claim(kvp.Key, kvp.Value.ToString()));
                }
            }

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
                default:
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
