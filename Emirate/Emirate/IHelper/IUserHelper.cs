
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

    }
}
