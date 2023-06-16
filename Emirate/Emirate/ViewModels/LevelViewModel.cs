using Emirate.Enum;
using Emirate.Models;

namespace Emirate.ViewModels
{
    public class LevelViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //public int? Departments { get; set; }
        public string Department { get; set; }
        public int? Level { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public Semester Semester { get; set;  }
        public int? DepartmentId { get; set; }
        public int TotalCreditLoad { get; set; }

        //public virtual List<Level>? Levels { get; set; }
    }
}
