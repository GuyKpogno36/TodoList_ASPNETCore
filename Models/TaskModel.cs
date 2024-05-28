using Todo_List_ASPNETCore.DAL;

namespace Todo_List_ASPNETCore.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateEcheance { get; set; }
        public TaskPriority Priorite { get; set; }
        public bool EstTerminee { get; set; }
        public int Categorie { get; set; }

        public IList<TASK> Tasks { get; set; }
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

}
