using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Data;
using System.Threading.Tasks;
using Todo_List_ASPNETCore.DAL;
using Todo_List_ASPNETCore.Helpers;
using Todo_List_ASPNETCore.Models;

namespace Todo_List_ASPNETCore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class Task : ControllerBase
    {
        private TodoListContext contexteEF = new(new DbContextOptions<TodoListContext>());

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = contexteEF.TASK.Include(t => t.Category).ToList();
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateTask(StringContent newTask)
        {
            var model = TodoHelper<TaskModel>.DecryptageData(newTask.ToString());

            // Logique pour ajouter la tâche à la base de données
            contexteEF.TASK.Add(new TASK
            {
                Task_Title = model.Title,
                Task_Desc = model.Description,
                Task_Deadline = model.Deadline,
                Task_Priority = model.Priority,
                Task_Status = model.status
            });
            contexteEF.SaveChanges();
            
            // Retournez une réponse appropriée, par exemple un code de statut HTTP 201 (Créé)
            return CreatedAtAction(nameof(GetAllTasks), null);
        }

        /*[HttpPost]
        public IActionResult CreateTask([FromBody] TaskModel task)
        {
            // Logique pour ajouter la tâche à la base de données
            contexteEF.TASK.Add(new TASK
            {
                Task_Title = task.Title,
                Task_Desc = task.Description,
                Task_Deadline = task.Deadline,
                Task_Priority = task.Priority,
                Task_Status = task.status
            });
            contexteEF.SaveChanges();
            // Retournez une réponse appropriée, par exemple un code de statut HTTP 201 (Créé)
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }*/

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskModel updatedTask)
        {
            // Logique pour mettre à jour la tâche dans la base de données
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

            // Retournez une réponse appropriée, par exemple un code de statut HTTP 200 (OK)
            return Ok("Task updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            // Logique pour supprimer la tâche de la base de données
            contexteEF.Remove(contexteEF.TASK.Single(p => p.Task_ID == id));
            contexteEF.SaveChanges();
            // Retournez une réponse appropriée, par exemple un code de statut HTTP 204 (Pas de contenu)
            return Ok("Task deleted successfully");
        }

        [HttpPut("{id}/complete")]
        public IActionResult MarkTaskAsCompleted(int id)
        {
            // Logique pour marquer la tâche comme complétée dans la base de données
            var task = contexteEF.TASK.Find(id);
            if (task == null) return NotFound("Task not found");

            task.Task_Status = true;
            contexteEF.SaveChanges();
            // Retournez une réponse appropriée, par exemple un code de statut HTTP 200 (OK)
            return Ok("Task closed successfully");
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = contexteEF.TASK.Include(t => t.Category).FirstOrDefault(t => t.Task_ID == id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        [HttpGet("completed")]
        public IActionResult GetCompletedTasks()
        {
            var tasks = contexteEF.TASK.Include(t => t.Category)
                                      .Where(t => t.Task_Status)
                                      .ToList();
            return Ok(tasks);
        }

        [HttpGet("overdue")]
        public IActionResult GetOverdueTasks()
        {
            var tasks = contexteEF.TASK.Include(t => t.Category)
                                      .Where(t => !t.Task_Status && t.Task_Deadline < DateTime.Now)
                                      .ToList();
            return Ok(tasks);
        }

        [HttpGet("filter")]
        public IActionResult FilterTasks(string category = null, TaskPriority? priority = null, bool? isCompleted = null)
        {
            var query = contexteEF.TASK.Include(t => t.Category).AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(t => t.Category.Category_Name == category);
            }

            if (priority.HasValue)
            {
                query = query.Where(t => t.Task_Priority == priority.Value);
            }

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.Task_Status == isCompleted.Value);
            }

            var tasks = query.ToList();
            return Ok(tasks);
        }

        [HttpGet("sort")]
        public IActionResult SortTasks(string sortBy, bool ascending = true)
        {
            var query = contexteEF.TASK.Include(t => t.Category).AsQueryable();

            switch (sortBy.ToLower())
            {
                case "Task_Deadline":
                    query = ascending ? query.OrderBy(t => t.Task_Deadline) : query.OrderByDescending(t => t.Task_Deadline);
                    break;
                case "priority":
                    query = ascending ? query.OrderBy(t => t.Task_Priority) : query.OrderByDescending(t => t.Task_Priority);
                    break;
                default:
                    query = ascending ? query.OrderBy(t => t.Task_Title) : query.OrderByDescending(t => t.Task_Title);
                    break;
            }

            var tasks = query.ToList();
            return Ok(tasks);
        }

    }
}
