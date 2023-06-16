using Emirate.Models;

namespace Emirate.ViewModels
{
    public class FacultyViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Faculty { get; set; }

        public virtual List<Faculty>? Faculties { get; set; }
    }
}
