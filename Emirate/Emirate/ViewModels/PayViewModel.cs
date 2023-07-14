using Emirate.EmirateEnums;
using Emirate.Models;

namespace Emirate.ViewModels
{
    public class PayViewModel
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public double Amount { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? RegNumber { get; set; }
        public string? LevelId { get; set; }
        public string? Level { get; set; }
        public string? FacultyId { get; set; }
        public string? Faculty { get; set; }
        public string? UserId { get; set; }
        public string? Department { get; set; }
        public string? DepartmentId { get; set; }
        public string? SchoolFees { get; set; }
        public string? SchoolFeesId { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime DatePaid { get; set; }


        //public List<SchoolFeesViewModel> SchoolFeesView { get; set; }
        //public List<ApplicationUserViewModel> ApplicationUserView { get; set; }
    }
}
