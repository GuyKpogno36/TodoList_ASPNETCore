using Todo_List_ASPNETCore.Models;
using Todo_List_ASPNETCore.DAL;

namespace Todo_List_ASPNETCore.Services
{
    public class TaskService
    {
        private readonly TodoListContext _context;

        public TaskService(TodoListContext context)
        {
            _context = context;
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
                Titre = taskEntity.Task_Title,
                Description = taskEntity.Task_Desc,
                DateEcheance = taskEntity.Task_Deadline,
                Priorite = ConvertToTaskPriority(taskEntity.Task_Priority.ToString()),
                
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
    }
}
