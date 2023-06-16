using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emirate.Models
{
	public class SchoolFee:BaseModel
	{
        public new Guid Id{ get; set; }

		public double Amount { get; set; }

		[Display(Name = "Faculty")]
		public int FacultyId { get; set; }
		[ForeignKey("FacultyId")]
		public virtual Faculty? Faculty { get; set; }

		public int LevelId { get; set; }
		[ForeignKey("LevelId")]
		public virtual Level? Level { get; set; }

	}
}
