using Emirate.EmirateEnums;
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
        public DateTime DatePaid { get; set; }
        public string? InvoiceNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
        public bool Active { get; set; }
       
    }
}
