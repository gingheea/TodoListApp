using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using TodoListApp.WebApp.Interfaces;
using TodoListApp.WebApp.Model;
using static System.Net.WebRequestMethods;

namespace TodoListApp.WebApp.Service
{
    public class DashboardService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public DashboardService(HttpClient http, IAuthService authService)
        {
            this._http = http;
            this._authService = authService;
        }

        public async Task<DashboardModel?> GetDashboardAsync()
        {
            var response = await this._http.GetAsync("api/home/dashboard");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {

                await this._authService.LogoutAsync();
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DashboardModel>();
        }

    }
}
