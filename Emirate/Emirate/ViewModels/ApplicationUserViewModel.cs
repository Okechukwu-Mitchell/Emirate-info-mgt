using Emirate.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emirate.ViewModels
{
    public class ApplicationUserViewModel
    {
        public bool IsAdmin { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? FullName => FirstName + " " + LastName;
		public bool RememberMe { get; set; }
		public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? RegNumber { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeactivated { get; set; }
        //public int DepartmentId { get; set; }
        public int LevelId { get; set; }
        public string? Password { get; set; }
        //public string? Department { get; set; }
        //public string? Gender { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
       // public int GenderId { get; set; }
		public string? UserName { get; set; }
		public string? CountryId { get; set; }
		public string? StateId { get; set; }
		public string? Image { get; set; }
        public string? Role { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int GenderId { get; set; }
        public CommonDropdowns? Gender { get; set; }

    }
}
