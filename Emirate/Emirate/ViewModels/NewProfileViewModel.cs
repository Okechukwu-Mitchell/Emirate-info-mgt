using Emirate.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using Emirate.Enum;

namespace Emirate.ViewModels
{
	public class NewProfileViewModel
	{

		public  int? Id { get; set; }
		public  string? FirstName { get; set; }
		public string? MiddleName { get; internal set; }
		public  string? LastName { get; set; }

		public int GenderId { get; set; }

		public int? DepartmentId { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? ProfilePicture { get; set; }
		[Required(ErrorMessage = "Please choose a file or folder from your device")]
		[Display(Name = "Profile Pix Url")]
		public IFormFile? ImageUrl { get; set; }
		public ApplicationUser? User { get; set; }
		public int LevelId { get; internal set; }
		public string? RegNumber { get; internal set; }
		public DateTime DateCreated { get; internal set; }
	}

}
