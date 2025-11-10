
namespace TodoListApp.WebApp.Service
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using Microsoft.AspNetCore.Components;
    using TodoListApp.WebApp.Model;

    public class TodoItemService
    {
        private readonly HttpClient _http;

        public TodoItemService(HttpClient http)
        {
            this._http = http;
        }

        public async Task<List<TodoItemModel>?> GetTasksByListIdAsync(int listId, int page = 1, int size = 10)
        {
            try
            {
                return await this._http.GetFromJsonAsync<List<TodoItemModel>>(
                    $"api/home/todolist/{listId}/todoitems?page={page}&size={size}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to load tasks for list {listId}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateTaskAsync(int listId, TodoItemCreateModel newTask)
        {
            var response = await this._http.PostAsJsonAsync($"api/home/todolist/{listId}/todoitems", newTask);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskAsync(int listId, int id, TodoItemUpdateModel updatedTask)
        {
            var response = await this._http.PutAsJsonAsync($"api/home/todolist/{listId}/todoitems/{id}", updatedTask);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTaskAsync(int listId, int taskId)
        {
            var response = await this._http.DeleteAsync($"api/home/todolist/{listId}/todoitems/{taskId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ToggleCompleteAsync(int listId, int id, bool isCompleted)
        {
            var response = await this._http.PutAsJsonAsync($"api/home/todolist/{listId}/todoitems/{id}/status", new { IsCompleted = isCompleted });
            return response.IsSuccessStatusCode;
        }
    }
}
