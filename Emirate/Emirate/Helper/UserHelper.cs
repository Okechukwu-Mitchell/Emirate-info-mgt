

using Emirate.DATABASE;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Emirate.Helper
{
    public class UserHelper: IUserHelper
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDb _context;

        public UserHelper(ApplicationDb context, UserManager<ApplicationUser> userManager)
        {
           _userManager = userManager;
            _context = context;
        }

        public ApplicationUser FindUserByEmail(string email)
        {
            var user = _userManager.Users.Where(x => x.Email == email).Include(f => f.Level).Include(f => f.Gender)
				.Include(b => b.Department).FirstOrDefault();
            return user;
        }

		public ApplicationUser FindUserById(string id)
		{
			var user = _context.Applications.Where(x => x.Email == id).Include(f => f.Level).Include(f => f.Gender)
				.Include(b => b.Department).FirstOrDefault();
			return user;
		}
		
        public async Task<ApplicationUser> CreateUserByAsync(ApplicationUserViewModel applicationUserViewModel)
        {
            if (applicationUserViewModel != null)
            {
                var user = new ApplicationUser()
                {
                    FirstName = applicationUserViewModel.FirstName,
                    LastName = applicationUserViewModel.LastName,
                    MiddleName = applicationUserViewModel.MiddleName,
                    LevelId = applicationUserViewModel.LevelId == 0?null: applicationUserViewModel.LevelId,
                    Password = applicationUserViewModel.Password,
                    ConfirmPassword = applicationUserViewModel.ConfirmPassword,
                    DepartmentId = applicationUserViewModel.DepartmentId,
                    DateOfBirth = applicationUserViewModel.DateOfBirth,
                    DateCreated = DateTime.Now,
                    PhoneNumber = applicationUserViewModel.PhoneNumber,
                    RegNumber = applicationUserViewModel.RegNumber,
                    GenderId = applicationUserViewModel.GenderId,
                    UserName = applicationUserViewModel.Email,
                    Email = applicationUserViewModel.Email,
                    UserRole = applicationUserViewModel.Role

                    
                };
                var result = await _userManager.CreateAsync(user, applicationUserViewModel.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, applicationUserViewModel.Role);
                    return user;
                }
            }
            return null;
        }

		public bool UpdateProfile(NewProfileViewModel applicationUser, string userName)
		{
			try
			{
				
				if (applicationUser != null && userName != null)
				{
					var userProfile = FindUserByEmail(userName);
					if (userProfile != null)
					{
                        userProfile.FirstName = applicationUser.FirstName;
                        userProfile.MiddleName = applicationUser.MiddleName;
                        userProfile.LastName = applicationUser.LastName;
                        userProfile.PhoneNumber = applicationUser.PhoneNumber;
                        userProfile.DateOfBirth = applicationUser.DateOfBirth;
                        userProfile.DepartmentId = applicationUser.DepartmentId;
                        userProfile.GenderId = applicationUser.GenderId;
                        userProfile.Email = applicationUser.Email;
                        userProfile.DateCreated = applicationUser.DateCreated;
                        userProfile.PhoneNumber = applicationUser.PhoneNumber;
                        userProfile.LevelId = applicationUser.LevelId;
                        userProfile.RegNumber = applicationUser.RegNumber;


						_context.Update(userProfile);
						_context.SaveChanges();
						return true;
					}
					return false;
				}
				else
				{
					return false;
				}
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		public ApplicationUser GetProfileInfo(string userName)
		{
            var user = FindUserByEmail(userName);
            var applicationUserViewModel = new ApplicationUser();
			 var  applicationiewModel = _context.Applications.Where(x => !x.IsDeactivated && x.Id == user.Id).Include(e => e.Department).Include(f => f.Level).Include(f => f.Gender).FirstOrDefault();
            if (applicationiewModel != null)
            {
                applicationUserViewModel = applicationiewModel;

			}
            return applicationUserViewModel;
		}

        public async Task<string[]> GetArreyListOfFaculties()
        {
            var faculties = _context.Faculty.Where(x => x.Id > 0 && !x.Deleted).OrderBy(o => o.Name).ToList();
            if (faculties.Any())
              return faculties.Select(t => t.Name).ToArray();

            return null;
        }
    }
}
