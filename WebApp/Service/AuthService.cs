using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;
using TodoListApp.WebApp.Interfaces;

namespace TodoListApp.WebApp.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            this._http = http;
            this._localStorage = localStorage;
            this._authStateProvider = authStateProvider;
        }

        public async Task<(bool Success, string? Error)> LoginAsync(string emailOrUsername, string password)
        {
            try
            {
                var response = await this._http.PostAsJsonAsync("api/auth/login", new { EmailOrUsername = emailOrUsername, Password = password });
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    try
                    {
                        var json = JsonSerializer.Deserialize<JsonElement>(content);

                        if (json.TryGetProperty("errors", out var errors))
                        {
                            var firstError = errors.EnumerateObject().First().Value[0].GetString();
                            return (false, firstError);
                        }

                        if (json.TryGetProperty("message", out var msg))
                        {
                            return (false, msg.GetString());
                        }
                    }
                    catch
                    {
                        return (false, content);
                    }

                    return (false, "Login failed");
                }

                var successJson = JsonSerializer.Deserialize<JsonElement>(content);
                string? token = null;

                if (successJson.TryGetProperty("token", out var tokenElement))
                {
                    token = tokenElement.GetString();
                }

                if (string.IsNullOrWhiteSpace(token))
                {
                    return (false, "Token not received from server");
                }

                await this._localStorage.SetItemAsync("authToken", token);
                ((ApiAuthenticationStateProvider)this._authStateProvider).NotifyUserAuthentication(token);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string? Error)> RegisterAsync(string email, string nickname, string password)
        {
            var response = await this._http.PostAsJsonAsync("api/auth/signup", new
            {
                Email = email,
                Nickname = nickname,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return (false, err);
            }

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            string? token = null;
            if (json.TryGetProperty("token", out var tokenElement))
            {
                token = tokenElement.GetString();
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return (false, "Token not received from server");
            }

            await this._localStorage.SetItemAsync("authToken", token);
            ((ApiAuthenticationStateProvider)this._authStateProvider).NotifyUserAuthentication(token);

            return (true, null);
        }

        public async Task<(bool Success, string? Error)> RegisterAsync(string email, string nickname, string password, string confirmPassword)
        {
            try
            {
                var response = await this._http.PostAsJsonAsync("api/auth/signup", new
                {
                    Email = email,
                    Nickname = nickname,
                    Password = password,
                    ConfirmPassword = confirmPassword
                });

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    try
                    {
                        var json = JsonSerializer.Deserialize<JsonElement>(content);

                        if (json.TryGetProperty("errors", out var errors))
                        {
                            var firstError = errors.EnumerateObject().First().Value[0].GetString();
                            return (false, firstError);
                        }

                        if (json.TryGetProperty("message", out var msg))
                        {
                            return (false, msg.GetString());
                        }
                    }
                    catch
                    {
                        return (false, content);
                    }

                    return (false, "Registration failed");
                }

                var successJson = JsonSerializer.Deserialize<JsonElement>(content);
                string? token = null;

                if (successJson.TryGetProperty("token", out var tokenElement))
                {
                    token = tokenElement.GetString();
                }

                if (string.IsNullOrWhiteSpace(token))
                {
                    return (false, "Token not received from server");
                }

                await this._localStorage.SetItemAsync("authToken", token);
                ((ApiAuthenticationStateProvider)this._authStateProvider).NotifyUserAuthentication(token);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task LogoutAsync()
        {
            await this._localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)this._authStateProvider).NotifyUserLogout();
        }

        public async Task<string?> GetTokenAsync() =>
            await this._localStorage.GetItemAsync<string>("authToken");
    }
}
