namespace Todo_List_ASPNETCore.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
