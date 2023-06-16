using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Emirate.Models
{
	public class StudentSchoolFeesRecord
	{
        public Guid Id { get; set; }
        [Required]
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }
		[Required]
		public Guid? SchoolFeeId { get; set; }
		[ForeignKey("SchoolFeeId")]
		public virtual SchoolFee? SchoolFee { get; set; }
	}
}
