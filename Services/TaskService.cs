using Todo_List_ASPNETCore.Models;
using Todo_List_ASPNETCore.DAL;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Todo_List_ASPNETCore.Services
{
    public class TaskService
    {
        private readonly TodoListContext _context;
        private readonly HttpClient _httpClient;

        public TaskService(TodoListContext context, HttpClient httpClient)
        {
            //_httpClient = httpClientFactory.CreateClient();
            _context = context;
            _httpClient = httpClient;
        }

        public TaskModel GetTaskById(int id)
        {
            var taskEntity = _context.TASK.Find(id);
            if (taskEntity == null)
            {
                throw new Exception("Task not found");
            }

            return ConvertToTaskModel(taskEntity);
        }

        private TaskModel ConvertToTaskModel(TASK taskEntity)
        {
            return new TaskModel
            {
                Id = taskEntity.Task_ID,
                Title = taskEntity.Task_Title,
                Description = taskEntity.Task_Desc,
                Deadline = taskEntity.Task_Deadline,
                Priority = ConvertToTaskPriority(taskEntity.Task_Priority.ToString()),

            };
        }

        private TaskPriority ConvertToTaskPriority(string priority)
        {
            if (Enum.TryParse<TaskPriority>(priority, true, out var taskPriority))
            {
                return taskPriority;
            }
            else
            {
                // Handle the case where the conversion fails
                // For example, you can return a default value or throw an exception
                throw new ArgumentException($"Invalid priority value: {priority}");
            }
        }

        public async Task<List<TASK>> GetTasksAsync() => await _httpClient.GetFromJsonAsync<List<TASK>>("api/task");

        public async Task<TASK> GetTaskByIdAsync(int id) => await _httpClient.GetFromJsonAsync<TASK>($"api/task/{id}");

        public async Task<HttpResponseMessage> CreateTaskAsync(StringContent httpContent) => await _httpClient.PostAsJsonAsync("api/task", httpContent);
        //public async Task<HttpResponseMessage> CreateTaskAsync(TaskModel task) => await _httpClient.PostAsJsonAsync("api/task", task);

        public async Task<HttpResponseMessage> UpdateTaskAsync(int id, TaskModel task) => await _httpClient.PutAsJsonAsync($"api/task/{id}", task);

        //public async Task<HttpResponseMessage> UpdateTaskAsync(int id, TaskModel task)
        //{
        //    var jsonString = "{" +
        //            "\"Id\":" + id + "," +
        //            "\"Title\":" + task.Title + "," +
        //            "\"Description\":" + task.Description + "," +
        //            "\"Deadline\":" + task.Deadline + "," +
        //            "\"Priority\":" + task.Priority + "," +
        //            "\"status\":" + task.status + "," +
        //            //"\"Categorie\":" + id + "," +
        //        "}";
        //    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        //    await _httpClient.PutAsync($"api/task/{id}", httpContent);
        //    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        //}

        public async Task<HttpResponseMessage> DeleteTaskAsync(int id) => await _httpClient.DeleteAsync($"api/task/{id}");
    }
}
