using Microsoft.AspNetCore.Identity;

namespace ToDoTestApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<MyTask>? Tasks { get; set; }
    }
}
