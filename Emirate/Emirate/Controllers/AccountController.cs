using Emirate.DATABASE;
using Emirate.Enum;
using Emirate.Helper;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Emirate.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDb _context;
        private readonly IUserHelper _userHelper;
        private readonly IDropdownHelper _dropdownHelper;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountController(ApplicationDb context, IDropdownHelper dropdownHelper, IUserHelper userHelper, 
			SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userHelper = userHelper;
            _signInManager = signInManager;
            _userManager = userManager;
			_dropdownHelper = dropdownHelper;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> objList = _context.Applications;  //Where(x => x.Email != null);

            return View(objList);
        }

        //GET - REGISTER
        [HttpGet]
        public IActionResult Register()
        {
			ViewBag.Level = _dropdownHelper.GetDropdownByKey(DropDownKey.Gender).Result;
            ViewBag.Gender = _dropdownHelper.GetDropdownByKey(DropDownKey.Gender).Result;
			ViewBag.Department = _dropdownHelper.GetDepartmentDropdown();
			

            return View();
        }

        //POST - REGISTER
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserViewModel applicationUserViewModel)
        {
			//ViewBag.Gender = _dropdownHelper.GetDropdownByKey(GenderEnums.Male);
			if (applicationUserViewModel != null)
            {
                var userEmail = await _userManager.FindByEmailAsync(applicationUserViewModel.Email);
                applicationUserViewModel.Role = "User";
                var addUser = await _userHelper.CreateUserByAsync(applicationUserViewModel);
                if (addUser != null)
                {
                    await _signInManager.PasswordSignInAsync(addUser, addUser.Password, true, true);
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(applicationUserViewModel);
        }




		//GET - ADMIN-REGISTER
		[HttpGet]
		public IActionResult AdminRegister()
		{
			ViewBag.Gender = _dropdownHelper.GetDropdownByKey(DropDownKey.Gender).Result;
			return View();
		}


		//POST - ADMIN-REGISTER
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AdminRegister(ApplicationUserViewModel model)
		{
			if (model != null)
			{
				var userEmail = _userHelper.FindUserByEmail(model.Email);
				model.Role = "Admin";
                var addUser = await _userHelper.CreateUserByAsync(model);
				if (addUser != null)
				{
					//await _signInManager.PasswordSignInAsync(addUser, addUser.Password,true, true);
				
					return RedirectToAction("Login");
				}
			}
			return View(model);
		}


		//GET - LOGIN
		[HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        //POST - LOGIN
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
			try
			{
				ViewData["ReturnUrl"] = returnUrl;
				if (model != null)
				{
					var user = _userHelper.FindUserByEmail(model.Email);
					if (user != null)
					{
						var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, true).ConfigureAwait(false);

                        if (result.Succeeded)
						{
							
                         
                            var userRole = await _userManager.GetRolesAsync(user);
                            if (userRole != null)
                            {
                                user.UserRole = userRole.FirstOrDefault();
                            }
                            if (user.UserRole.ToLower() == "admin")
                            {
                                return RedirectToAction("AdminDashBoard", "Admin");
                            }
                            else
                            {
                                return RedirectToAction("UserDashBoard","User");
                            }

                        }
                        ViewBag.Message = "Invalid Loggin Attempt";
						ModelState.AddModelError(string.Empty, "Invalid Loggin Attempt");
					}

				}
				return View();
			}
			catch (Exception ex)
			{
                throw ex;
			}
		}

      
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
            
        }
       

        //GET - ADMINLOGIN
        //[HttpGet]
        //public IActionResult AdminLogin()
        //{
        //	return View();
        //}


        //POST - ADMINLOGIN
        //[HttpPost]
        //public async Task<IActionResult> AdminLogin(ApplicationUserViewModel model)
        //{
        //	try
        //	{
        //		if (model != null)
        //		{
        //			var user =  _userHelper.FindUserByEmail(model.Email);
        //			if (user != null)
        //			{
        //				var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, true).ConfigureAwait(false);
        //				if (result.Succeeded)
        //				{
        //					var userRole = await _userManager.GetRolesAsync(user);
        //					if (userRole != null) {
        //						user.UserRole = userRole.FirstOrDefault();
        //                          }
        //					if (user.UserRole.ToLower() == "admin")
        //					{
        //                              return RedirectToAction("AdminDashBoard", "Admin");
        //                          }
        //					else
        //					{
        //                              return RedirectToAction("UserDashBoard", "User");
        //                          }

        //				}
        //				ViewBag.Message = "Invalid Loggin Attempt";
        //				ModelState.AddModelError(string.Empty, "Invalid Loggin Attempt");
        //			}

        //		}
        //		return View(model);
        //	}
        //	catch (Exception ex)
        //	{
        //		throw ex;
        //	}
        //}



        //public IActionResult ContactUs()
        //      {
        //          return View();
        //      }


    }
}
