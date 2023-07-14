
using Emirate.EmirateEnums;
using Emirate.Models;
using Emirate.ViewModels;
using ApplicationUser = Emirate.Models.ApplicationUser;

namespace Emirate.IHelper
{
    public interface IUserHelper
    {
        Task<ApplicationUser> CreateUserByAsync(ApplicationUserViewModel applicationUserViewModel);
		ApplicationUser FindUserByEmail(string username);
		//Task<ApplicationUser> FindUserByEmailAsync(string email);
		ApplicationUser FindUserById(string id);
		//ApplicationUser FindUserByEmail(string email);
		bool UpdateProfile(NewProfileViewModel applicationUser, string userName);
		//ApplicationUser FindByUserName(string username);
		ApplicationUser GetProfileInfo(string userName);
		Task<string[]> GetArreyListOfFaculties();
		//string GetDescription( PaymentStatus value);
		string GetEnumDescription(System.Enum value);
        List<StudentCourseViewModel> GetListOfCourse();
		List<StudentCourseViewModel> MyCourses(string username);
		List<StudentCourseViewModel> MyCoursesReg(string username);
		List<StudentCourseViewModel> GetSelectedCourses(string username, List<int> selectedCourseIds);
		//List<StudentCourseViewModel> MyCoursesReg(string username, List<int> selectedCourseIds);
		//StudentPaymentViewModel MyStudentPaymentRecord();
		StudentPaymentViewModel MyStudentPaymentRecord(string currentLoggedInUser);

    }
}
