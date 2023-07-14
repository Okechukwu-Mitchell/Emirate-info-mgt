using Emirate.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Emirate.Models
{
	public class ApplicationUser : IdentityUser
	{	
		public bool IsAdmin { get; set; }
		public string? FirstName { get; set; }
		public string? MiddleName { get; set; }
		public string? FullName => FirstName + " " + MiddleName + " " + LastName;

		public string? LastName { get; set; }
		public string? RegNumber { get; set; }
		public string? Address { get; set; }
		public DateTime DateOfBirth { get; set; }
		public DateTime DateCreated { get; set; }
		public bool IsDeactivated { get; set; }
		public int? DepartmentId { get; set; }
		[ForeignKey("DepartmentId")]
		public virtual Department? Department { get; set; }
        public int? LevelId { get; set; }
        [ForeignKey("LevelId")]
		public virtual Level? Level { get; set; }
		public int? GenderId { get; set; }
        [ForeignKey("GenderId")]
        public virtual CommonDropdowns? Gender { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string? Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string? ConfirmPassword { get; set; }
		public bool RememberMe { get; set; }
		public string? CountryId { get; set; }
		public string? StateId { get; set; }
		public string? Image { get; set; }

		[NotMapped]
		public string? UserRole { get; set; }
		[NotMapped]
		public string? PaymentStatus { get; set; }
	}
}


