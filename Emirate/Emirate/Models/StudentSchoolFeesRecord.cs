using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Emirate.EmirateEnums;

namespace Emirate.Models
{
	public class StudentSchoolFeesRecord
	{
        public Guid Id { get; set; }
        [Required]
        public double Amount { get; set; }
        public string? UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }
		[Required]
		public Guid? SchoolFeeId { get; set; }
		[ForeignKey("SchoolFeeId")]
		public virtual SchoolFee? SchoolFee { get; set; }
        public string? InvoiceNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
