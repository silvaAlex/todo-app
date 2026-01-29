using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ToDoApp.Web.Client;
using ToDoApp.Web.Models.Tasks;

namespace ToDoApp.Web.Pages
{
    public class TaskService
    {
        private ApiClient _api;
        public TaskService()
        {
            _api = new ApiClient();
            var token = Helpers.AuthSession.GetToken();
            _api.SetAuthToken(token);
        }

        public async Task<List<TaskResponse>> GetAllTasksAsync()
        {
            var response = await _api.Client.GetAsync("api/tasks").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new List<TaskResponse>();
            return await response.Content.ReadFromJsonAsync<List<TaskResponse>>() ?? new List<TaskResponse>();
        }

        public async Task<TaskResponse> GetTasksByIdAsync(Guid id)
        {
            var response = await _api.Client.GetAsync($"api/tasks/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<TaskResponse>();
        }

        public async Task<TaskResponse> CreateTaskAsync(TaskRequest request)
        {
            var response = await _api.Client.PostAsJsonAsync($"api/tasks", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<TaskResponse>();
        }

        public async Task<TaskResponse> UpdateTaskAsync(Guid id, TaskRequest request)
        {
            var response = await _api.Client.PutAsJsonAsync($"api/tasks/{id}", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<TaskResponse>();
        }

        public async Task<bool> CompleteTaskAsync(Guid id)
        {
            var response = await _api.Client.PatchAsJsonAsync($"api/tasks/{id}", "");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletTaskAsync(Guid id)
        {
            var response = await _api.Client.DeleteAsync($"api/tasks/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<TaskResponse>> GetTasksByCategoryAsync(string category)
        {
            var response = await _api.Client.GetAsync($"api/tasks/category/{category}");

            if (!response.IsSuccessStatusCode)
                return new List<TaskResponse>();
            return await response.Content.ReadFromJsonAsync<List<TaskResponse>>() ?? new List<TaskResponse>();
        }
    }
}