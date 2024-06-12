using Todo_List_ASPNETCore.DAL;

namespace Todo_List_ASPNETCore.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskPriority Priority { get; set; }
        public bool status { get; set; }
        public int Categorie { get; set; }

        public IList<TASK> Tasks { get; set; }
        public IList<CATEGORY> Categories { get; set; }
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    public class DashboardModel
    {
        public List<TASK> PendingTasks { get; set; }
        public List<TASK> CompletedTasks { get; set; }
        public List<TASK> OverdueTasks { get; set; }
        public List<TaskPriority> Priorities { get; set; }
        public List<CATEGORY> Categories { get; set; }
    }

}
