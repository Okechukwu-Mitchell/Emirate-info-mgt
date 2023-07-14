using Emirate.EmirateEnums;
using Emirate.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emirate.ViewModels
{
    public class StudentPaymentViewModel
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string? Username { get; set; }
        public string? RegNumber { get; set; }
       // public string? Department { get; set; }
        public Department? Department { get; set; }
        //public string? Level { get; set; }
        public Level? Level { get; set; }
        public Faculty? Faculty { get; set; }
        public string? StudentName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string? PaymentDocument { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public string? ReferenceNumber { get; set; }
        public Guid? SchoolFeesId { get; set; }
        public SchoolFee? SchoolFees { get; set; }
        public DateTime DatePaid { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public bool Active { get; set; }

        public ICollection<StudentPayment>? SchoolFeesReceipts { get; set; }
    }
}
