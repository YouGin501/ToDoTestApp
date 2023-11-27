using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoTestApp.DTO
{
    public class MyTaskDTO
    {
        public int ID { get; set; }
        public string Title { get; set; } = "Title";
        public string Description { get; set; } = "Description";

        public bool Done { get; set; } = false;

        public byte LevelOfImportance { get; set; } = 2;

        public string UserId { get; set; }
    }
}
