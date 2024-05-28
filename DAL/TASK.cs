using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Todo_List_ASPNETCore.Models;

namespace Todo_List_ASPNETCore.DAL
{
    public class TASK
    {
        [Key]
        public int Task_ID { get; set; }
        public string Task_Title { get; set; }
        [AllowNull]
        public string Task_Desc { get; set; }
        public DateTime Task_Deadline { get; set; }
        public TaskPriority Task_Priority { get; set; }
        public bool Task_Status { get; set; }

        public int Category_ID { get; set; }
        public virtual CATEGORY Category { get; set; }
    }
}
