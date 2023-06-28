using Emirate.Models;
using System.Reflection.Metadata;

namespace Emirate.ViewModels
{
	public class DashboardViewModel
	{
		
	public IEnumerable<Faculty> faculty { get; set; }
	public int TotalFaculty { get { return faculty.Count(); } }
	public IEnumerable<Department> department { get; set; }
	public int TotalDepartment { get { return department.Count(); } }
	public IEnumerable<ApplicationUser> applicationUser { get; set; }
	public int TotalApplicationUser { get { return applicationUser.Count(); } }

	}
}
