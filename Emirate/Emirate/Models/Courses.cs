using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Emirate.Enum;

namespace Emirate.Models
{
	public class Courses: BaseModel
	{
		public int CourseUnit { get; set; }

		[Display(Name = "Department")]
		public int? DepartmentId { get; set; }
		[ForeignKey("DepartmentId")]
		public virtual Department? Department { get; set; }

		[Display(Name = "Level")]
		public int? LevelId { get; set; }
		[ForeignKey("LevelId")]
		public virtual Level? Level { get; set; }
		public Semester Semester { get; set; }


    }
}
