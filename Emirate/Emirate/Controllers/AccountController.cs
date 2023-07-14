using Emirate.DATABASE;
using Emirate.Enum;
using Emirate.Helper;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Emirate.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDb _context;
        private readonly IUserHelper _userHelper;
        private readonly IAdminHelper _adminHelper;
        private readonly IDropdownHelper _dropdownHelper;
        private readonly IEmailHelper _emailHelper;
        private readonly IDescriptionHelper _descriptionHelper;
        private readonly IEmailSettings _emailSettings;
       


        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountController(ApplicationDb context, IDropdownHelper dropdownHelper, IDescriptionHelper descriptionHelper, IAdminHelper adminHelper, IUserHelper userHelper, IEmailHelper emailHelper,IEmailSettings emailSettings, 
			SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userHelper = userHelper;
            _signInManager = signInManager;
            _userManager = userManager;
			_dropdownHelper = dropdownHelper;
			_emailHelper = emailHelper;
            _descriptionHelper = descriptionHelper;
            _adminHelper = adminHelper;
            _emailSettings = emailSettings;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> objList = _context.Applications;  //Where(x => x.Email != null);

            return View(objList);
        }


        //public IActionResult SendEmail()
        //{
        //    string to = "recipient@example.com";
        //    string subject = "Hello";
        //    string body = "This is the email body.";

        //    _emailService.SendEmail(to, subject, body);

        //    return RedirectToAction("Index");
        //}


        //public async Task<IActionResult> SendEmail()
        //{
        //    string toEmail = "recipient@example.com";
        //    string subject = "Hello";
        //    string body = "This is the email body.";

        //    await _emailHelper.SendEmailAsync(toEmail, subject, body);

        //    return RedirectToAction("Index");
        //}



        //GET - REGISTER
        [HttpGet]
        public IActionResult Register()
        {
			ViewBag.Level = _dropdownHelper.DropdownOfLevels();
            ViewBag.Gender = _dropdownHelper.GetDropdownByKey(DropDownKey.Gender).Result;
			ViewBag.Department = _dropdownHelper.GetDepartmentDropdown();
			

            return View();
        }

        //POST - REGISTER
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserViewModel applicationUserViewModel)
        {
            ViewBag.Level = _dropdownHelper.DropdownOfLevels();
            ViewBag.Gender = _dropdownHelper.GetDropdownByKey(DropDownKey.Gender).Result;
            ViewBag.Department = _dropdownHelper.GetDepartmentDropdown();

            if (ModelState.IsValid)
            {
                if (applicationUserViewModel.Password != applicationUserViewModel.ConfirmPassword)
                {
					ModelState.AddModelError("ConfirmPassword", "The password and confirm password do not match.");
					return View(applicationUserViewModel);
				}
                
            }
			

			if (applicationUserViewModel != null)
            {
                var userEmail = await _userManager.FindByEmailAsync(applicationUserViewModel.Email);
                applicationUserViewModel.Role = "User";
                var addUser = await _userHelper.CreateUserByAsync(applicationUserViewModel);
                if (addUser != null)
                {
                    var sendEmail = _emailHelper.SendAdminEmail(addUser);
					TempData["Message"] = "Registered Successfully";
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
		public async Task<IActionResult> AdminRegister(ApplicationUserViewModel model, string base64)
		{
			ViewBag.Gender = _dropdownHelper.GetDropdownByKey(DropDownKey.Gender).Result;

			if (!ModelState.IsValid)
			{
				if (model.Password != model.ConfirmPassword)
				{
					ModelState.AddModelError("ConfirmPassword", "The password and confirm password do not match.");
					return View(model);
				}

			}

			if (model.Email != null)
			{
				var userEmail = _adminHelper.FindUserByEmailAsync(model.Email).Result;
                if (userEmail != null)
                {
                    model.IsError = true;
					TempData["Message"] = "Username already exist.";
                    return View(model);
                }
				model.Role = "Admin";
                var addUser = await _adminHelper.AdminRegister(model, base64);
				if (addUser != null)
				{
                    await _signInManager.PasswordSignInAsync(addUser, addUser.Password, true, true);
					return RedirectToAction("Login", "Account");
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
                            if (user.UserRole?.ToLower() == "admin")
                            {
                                return RedirectToAction("AdminDashBoard", "Admin");
                            }
                            else
                            {
                                return RedirectToAction("UserDashBoard", "User");
                            }
                        }
                        ViewBag.Message = "Invalid Login Attempt";
                        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
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



        //// User logout
        //[Route("/user/logout")]
        //public IActionResult UserLogout()
        //{
        //    // User logout logic
        //    // Clear user-specific session, authentication, or any other data

        //    _signInManager.SignOutAsync();
        //    return RedirectToAction("Login", "Account");
        //}

        //// Admin logout
        //[Route("/admin/logout")]
        //public IActionResult AdminLogout()
        //{
        //    // Admin logout logic
        //    // Clear admin-specific session, authentication, or any other data

        //    _signInManager.SignOutAsync();
        //    return RedirectToAction("Login", "Account");
        //}



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


        [HttpGet]
        public JsonResult EditUserByID(string EditUserId)
        {
            if (EditUserId != null)
            {
                var user = _context.Applications.Where(c => c.Id == EditUserId && !c.IsDeactivated).Include(d => d.Department).FirstOrDefault();
                if (user != null)
                {
                    return Json(new {isError = false, data = user });
                }
            }
            return null;

        }


        [HttpPost]
        public JsonResult EditUser(string userDetails)
        {

            if (userDetails != null)
            {
                var user = JsonConvert.DeserializeObject<ApplicationUserViewModel>(userDetails);
                if (user != null)
                {
                    var result = _descriptionHelper.EditUser(user);
                    if (result != null)
                    {
                        return Json(new { isError = false, msg = "Updated Successfully" });
                    }
                }
                return Json(new { isError = true, msg = "Unable to Update" });
            }
            return Json(new { isError = true, msg = "Error Occurred" });



        }


        [HttpGet]
        public JsonResult DeleteUserByID(string DeleteUserId)
        {
            if (DeleteUserId != null)
            {
                var user = _context.Applications.Where(c => c.Id == DeleteUserId).FirstOrDefault();
                if (user != null)
                {
                    return Json(user);
                }
            }
            return null;

        }


        [HttpPost]
        public JsonResult DeleteUser(string id)
        {

            if (id != null)
            {
                var result = _descriptionHelper.DeleteUser(id);
                if (result)
                {
                    return Json(new { isError = false, msg = "Deleted Successfully" });
                }
            }
            return Json(new { isError = true, msg = "Error Occurred" });

        }

    }
}
