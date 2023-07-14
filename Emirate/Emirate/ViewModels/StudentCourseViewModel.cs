using Emirate.Enum;
using Emirate.Models;

namespace Emirate.ViewModels
{
    public class StudentCourseViewModel
    {
        public int Id { get; set; }
        public string? StudentId { get; set; }
        public int SchoolFeeId { get; set; }
        public  ApplicationUser? ApplicationUser { get; set; }
        public  SchoolFee? SchoolFee { get; set; }
        public string? Course { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public string? StudentName { get; set; }
        public string? StudentFullName { get; set; }
        public string? Name { get; set; }
        public string? LevelName { get; set; }
        public int CourseId { get; set; }
        public bool SelectedCourseId { get; set; }
        public string? SelectedCourse { get; set; }
        public string? PaymentStatus { get; set; }
        public string? AvailableCourses { get; set; }
        public string? DepartmentName { get; set; }
        public Department? Department { get; set; }
        public int? LevelId { get; set; }
        public Level? Level { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public Semester Semester { get; set; }
        public int? DepartmentId { get; set; }
        public int CourseUnit { get; set; }
        public int TotalCourseUnit { get; set; }
        public List<Level>? Levels { get; set; }

    }
}
