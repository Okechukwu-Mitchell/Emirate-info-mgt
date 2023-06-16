using Emirate.Enum;
using Emirate.Models;

namespace Emirate.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LevelName { get; set; }
        public string? DepartmentName { get; set; }
        public Department? Department { get; set; }
        public int? LevelId { get; set; }
        public Level? Level { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public Semester Semester { get; set; }
        public int? DepartmentId { get; set; }
        public int CourseUnit { get; set; }
        public  List<Level>? Levels { get; set; }
    }
}
