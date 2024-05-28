using System.ComponentModel.DataAnnotations;

namespace Todo_List_ASPNETCore.DAL
{
    public class CATEGORY
    {
        [Key]
        public int Category_ID { get; set; }
        public string Category_Name { get; set; }
    }
}
