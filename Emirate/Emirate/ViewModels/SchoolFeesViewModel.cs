using Emirate.EmirateEnums;
using Emirate.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Emirate.ViewModels
{
    public class SchoolFeesViewModel
    {
        public  Guid Id { get; set; }
        public double Amount { get; set; }
        public double Amount1 { get; set; }
        public double Amount2 { get; set; }
        [Display(Name = "Faculty")]
        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
        public int LevelId { get; set; }
        public int LevelId1 { get; set; }
        public int LevelId2 { get; set; }
        public Level? Level { get; set; }
       
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public DateTime DatePaid { get; set; }
        public string? InvoiceNumber { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string? UserId { get; set; }
        public  ApplicationUser? User { get; set; }
        public bool Active { get; set; }
        public  List<SchoolFee>? SchoolFees { get; set; }
        public  List<Faculty>? Faculties { get; set; }
        public  List<Department>? Departments { get; set; }

    }
}
