using Emirate.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emirate.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Faculty { get; set; }
        public int? FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public virtual List<Faculty>? Faculties { get; set; }
        public virtual List<Department>? Departments { get; set; }
    }
}
