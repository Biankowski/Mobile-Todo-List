using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string? TodoName { get; set; }


    }
}
