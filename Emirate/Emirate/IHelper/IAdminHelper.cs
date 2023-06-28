using Emirate.Models;
using Emirate.ViewModels;

namespace Emirate.IHelper
{
	public interface IAdminHelper
	{
		//Task<ApplicationUser> CreateUserByAsync(ApplicationUserViewModel applicationUserViewModel);
		Task<ApplicationUser> FindByUserNameAsync(string username);
		Task<ApplicationUser> FindUserByEmailAsync(string email);
		Task<ApplicationUser> FindUserByIdAsync(string id);
		ApplicationUser FindUserByEmail(string email);
		bool DeleteFaculty(FacultyViewModel facultyDetails);
		bool EditDepartment(DepartmentViewModel deptDetails);
		bool DeleteDepartment(int id);
		bool EditLevel(LevelViewModel levelDetails);
		bool DeleteLevel(LevelViewModel levelDetails);
		List<LevelViewModel> GetListOfLevels();
		List<CourseViewModel> GetListOfCourse();
		bool Courses(CourseViewModel courseDetail);
		bool EditCourses(CourseViewModel courseDetail);
		bool DeleteCourses(int id);
		//List<ApplicationUserViewModel> GetListOfStudent();
		public Task<ApplicationUser> AdminRegister(ApplicationUserViewModel model, string base64);
    }
}
