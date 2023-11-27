using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoTestApp.Models
{
    public class MyTask
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(150), Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; } = "Title";

        [MaxLength(300), Column(TypeName = "nvarchar(300)")]
        public string Description { get; set; } = "Description";

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public bool Done { get; set; } = false;

        public byte LevelOfImportance { get; set; } = 2;

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
