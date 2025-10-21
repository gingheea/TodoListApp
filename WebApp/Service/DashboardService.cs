using System.Net.Http.Json;
using TodoListApp.WebApp.Model;

namespace TodoListApp.WebApp.Service
{
    public class DashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<DashboardModel?> GetDashboardAsync()
        {
            return await this._httpClient.GetFromJsonAsync<DashboardModel>("api/home/dashboard");
        }
    }
}
