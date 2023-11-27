namespace ToDoTestApp.Models
{
    public class Filter
    {
        public byte? LevelOfImportance { get; set; } = null;

        public bool All { get; set; } = false;
        public bool Done { get; set; } = false;
    }
}
