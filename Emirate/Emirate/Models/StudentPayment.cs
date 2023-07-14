using Emirate.EmirateEnums;
using Emirate.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emirate.Models
{
    public class StudentPayment : BaseModel
    {
        public int DocumentId { get; set; }
        public string? Username { get; set; }
        public string? StudentName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string? PaymentDocument { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public string? ReferenceNumber { get; set; }
        public Guid? SchoolFeesId { get; set; }
        [ForeignKey("SchoolFeesId")]
        public virtual SchoolFee? SchoolFees { get; set; }
        public  DateTime DatePaid { get; set; }
        //public int? UserId { get; set; }
        //[ForeignKey("UserId")]
        //public virtual User? User { get; set; }
        public bool Active { get; set; }
     
    }
}

