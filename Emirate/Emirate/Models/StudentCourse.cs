using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Emirate.Enum;

namespace Emirate.Models
{
	public class StudentCourse
	{
        public int Id { get; set; }

        [Display(Name = "Level")]
        public int? LevelId { get; set; }
        [ForeignKey("LevelId")]
        public virtual Level? Level { get; set; }

        [Display(Name = "Department")]

		public int DepartmentId { get; set; }
		[ForeignKey("DepartmentId")]
		public virtual Department? Department { get; set; }

		[Display(Name = "User")]
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser? ApplicationUser { get; set; }
		public DateTime DateRegistered { get; set; }

        public bool Deleted { get; set; }
		public Semester Semester { get; set; }
		public CourseProgress CourseInProgress { get; set; }

	}
}
