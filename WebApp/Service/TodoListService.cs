namespace TodoListApp.WebApp.Service
{
    using System.Net.Http.Json;
    using TodoListApp.WebApp.Model;

    public class TodoListService
    {
        private readonly HttpClient _http;

        public TodoListService(HttpClient http)
        {
            this._http = http;
        }

        public async Task<List<TodoListModel>> GetUserListsAsync()
        {
            try
            {
                var result = await this._http.GetFromJsonAsync<List<TodoListModel>>("api/home/todolist");
                return result ?? new List<TodoListModel>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Network error while fetching todo lists: {ex.Message}");
                return new List<TodoListModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected error: {ex.Message}");
                return new List<TodoListModel>();
            }
        }

        public async Task<TodoListModel?> GetListByIdAsync(int id)
        {
            return await this._http.GetFromJsonAsync<TodoListModel>($"api/home/todolist/{id}");
        }

        public async Task<bool> CreateListAsync(TodoListCreateModel model)
        {
            var response = await this._http.PostAsJsonAsync("api/home/todolist", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteListAsync(int id)
        {
            var response = await this._http.DeleteAsync($"api/home/todolist/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateListTitleAsync(int id, string newTitle)
        {
            var response = await this._http.PutAsJsonAsync($"api/home/todolist/{id}", new { Title = newTitle });
            return response.IsSuccessStatusCode;
        }
    }
}
