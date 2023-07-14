using Emirate.EmirateEnums;
using Emirate.Models;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emirate.ViewModels
{
    public class StudentSchoolFeesRecordViewModel
    {
        public Guid Id { get; set; }
        [Required]
        //public int UserId { get; set; }
        public double Amount { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
        [Required]
        public Guid? SchoolFeeId { get; set; }
        public PaymentStatus Status { get; set; }

        public string? InvoiceNumber { get; set; }
      
        public PaymentStatus PaymentStatus { get; set; }
        public virtual SchoolFee? SchoolFee { get; set; }

    }

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public string Level { get; set; }
        // Other personal details
    }

    //public class Department
    //{
    //    public int DepartmentId { get; set; }
    //    public string Name { get; set; }
    //}

    //public class Faculty
    //{
    //    public int FacultyId { get; set; }
    //    public string Name { get; set; }
    //}

    //public class SchoolFeeViewModel
    //{
    //    public int FeeId { get; set; }
    //    public int DepartmentId { get; set; }
    //    public Department Department { get; set; }
    //    public int FacultyId { get; set; }
    //    public Faculty Faculty { get; set; }
    //    public string Level { get; set; }
    //    public decimal Amount { get; set; }
    //}

}
