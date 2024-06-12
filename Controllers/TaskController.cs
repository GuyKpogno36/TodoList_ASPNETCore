using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Todo_List_ASPNETCore.API;
using Todo_List_ASPNETCore.DAL;
using Todo_List_ASPNETCore.Models;
using Todo_List_ASPNETCore.Services;

namespace Todo_List_ASPNETCore.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskService _taskApiService;
        private TodoListContext contexteEF = new(new DbContextOptions<TodoListContext>());

        public TaskController(TaskService taskApiService)
        {
            _taskApiService = taskApiService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskApiService.GetTasksAsync();
            var tasks_model = new TaskModel{
                Tasks = tasks,
                Categories = contexteEF.CATEGORY.ToList()
        };
            return View(tasks_model);
        }

        public async Task<JsonResult> Details(int id)
        {
            var task = await _taskApiService.GetTaskByIdAsync(id);
            return Json(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskModel task)
        {
            contexteEF.TASK.Add(new TASK
            {
                Task_Title = task.Title,
                Task_Desc = task.Description,
                Task_Deadline = task.Deadline,
                Task_Priority = task.Priority,
                Task_Status = task.status,
                Category_ID = task.Categorie
            });
            contexteEF.SaveChanges();

            task.Tasks = await _taskApiService.GetTasksAsync();
            task.Categories = contexteEF.CATEGORY.ToList();
            return View(nameof(Index), task);
        }

        /*[HttpPost]
        public async Task<IActionResult> Create(TaskModel task)
        {
            var jsonString = "{" +
                    "\"Title\":" + task.Title + "," +
                    "\"Description\":" + task.Description + "," +
                    "\"Deadline\":" + task.Deadline + "," +
                    "\"Priority\":" + task.Priority + "," +
                    "\"status\":" + task.status + "," +
                "}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _taskApiService.CreateTaskAsync(httpContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            task.Tasks = await _taskApiService.GetTasksAsync();
            return View(nameof(Index), task);
        }*/

        public async Task<IActionResult> Select(int id)
        {
            var task = await _taskApiService.GetTaskByIdAsync(id);
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskModel updatedTask)
        {
            if (updatedTask == null || updatedTask.Id != id) return BadRequest("Invalid task data");

            var task = contexteEF.TASK.Find(id);
            if (task == null) return NotFound("Task not found");

            task.Task_Title = updatedTask.Title;
            task.Task_Desc = updatedTask.Description;
            task.Task_Deadline = updatedTask.Deadline;
            task.Task_Priority = updatedTask.Priority;
            task.Task_Status = updatedTask.status;
            task.Category_ID = updatedTask.Categorie;

            contexteEF.TASK.Update(task);
            contexteEF.SaveChanges();

            updatedTask.Tasks = await _taskApiService.GetTasksAsync();
            updatedTask.Categories = contexteEF.CATEGORY.ToList();
            return View(nameof(Index), task);
        }
        
        /*[HttpPost]
        public async Task<IActionResult> Edit(int id, TaskModel task)
        {
            var response = await _taskApiService.UpdateTaskAsync(id, task);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }*/

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskApiService.GetTaskByIdAsync(id);
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _taskApiService.DeleteTaskAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var task = contexteEF.TASK.Find(id);
            if (task == null) return NotFound("Task not found");

            task.Task_Status = true;
            contexteEF.SaveChanges();
            
            return View();
        }

        /*public async Task<IActionResult> Dashboard()
        {
            var tasks = await _taskApiService.GetTasksAsync();

            var viewModel = new DashboardModel
            {
                PendingTasks = tasks.Where(t => t.Task_Status == false).ToList(),
                CompletedTasks = tasks.Where(t => t.Task_Status == true).ToList(),
                OverdueTasks = tasks.Where(t => t.Task_Deadline < DateTime.Now && t.Task_Status == false).ToList(),
                Priorities = tasks.Select(t => t.Task_Priority).Distinct().ToList(),
                Categories = tasks.Select(t => t.Category).Distinct().ToList()
            };

            return View(viewModel);
        }*/

        public async Task<IActionResult> Dashboard(string priority, string category, string sortOrder)
        {
            var tasks = await _taskApiService.GetTasksAsync();

            if (!string.IsNullOrEmpty(priority))
            {
                tasks = tasks.Where(t => t.Task_Priority.ToString() == priority).ToList();
            }

            if (!string.IsNullOrEmpty(category))
            {
                tasks = tasks.Where(t => t.Category.Category_ID.ToString() == category).ToList();
            }

            switch (sortOrder)
            {
                case "deadline":
                    tasks = tasks.OrderBy(t => t.Task_Deadline).ToList();
                    break;
                case "priority":
                    tasks = tasks.OrderBy(t => t.Task_Priority).ToList();
                    break;
            }

            var viewModel = new DashboardModel
            {
                PendingTasks = tasks.Where(t => !t.Task_Status).ToList(),
                CompletedTasks = tasks.Where(t => t.Task_Status).ToList(),
                OverdueTasks = tasks.Where(t => t.Task_Deadline < DateTime.Now && !t.Task_Status).ToList(),
                Priorities = tasks.Select(t => t.Task_Priority).Distinct().ToList(),
                Categories = tasks.Select(t => t.Category).Distinct().ToList()
            };

            return View(viewModel);
        }


    }
}
