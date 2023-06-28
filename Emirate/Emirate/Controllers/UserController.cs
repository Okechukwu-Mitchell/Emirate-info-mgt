using Emirate.DATABASE;
using Emirate.Enum;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Emirate.Controllers
{
	public class UserController : Controller
	{
		private ApplicationDb _context;
		private readonly IUserHelper _userHelper;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IDropdownHelper _dropdownHelper;

		public UserController(ApplicationDb context,  IUserHelper userHelper, SignInManager<ApplicationUser> signInManager, IDropdownHelper dropdownHelper)
		{
			_context = context;
			_userHelper = userHelper;
			_signInManager = signInManager;
			_dropdownHelper = dropdownHelper;
		}
		public IActionResult Index()
		{
			return View();
		}

		//GET - EDIT
		public IActionResult EditProfile(int? id)
		{
            if (id == null || id == 0)
            {
				return NotFound();
			}
			var obj = _context.Applications.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			return View();
		}

		//POST - EDIT
		[HttpPost]
		public IActionResult EditProfile(ApplicationUser obj)
		{
			try
			{ 
				if (ModelState.IsValid)
				{
					_context.Applications.Update(obj);
					_context.SaveChanges();
					return RedirectToAction("Profile");
				}
				return View();
			}
            catch (Exception ex)
            {
				throw ex;
			}
		}


        [Authorize(Roles = "User")]
        public IActionResult UserDashBoard()
		{
			return View();
		}



		[HttpGet]
        public IActionResult Profile()
        
		{
			var userName = User.Identity.Name;
			var profile = _userHelper.GetProfileInfo(userName);
			return View(profile);
        }
		

		[HttpPost]
		public async Task<IActionResult> Profile(NewProfileViewModel applicationUser)
		{
			//ViewBag.Gender = await _dropdownHelper.GetDropdownByKey(GenderEnums.Male);
			try
			{
				if (applicationUser.ProfilePicture != null)
				{
					if (!applicationUser.ImageUrl.ContentType.Contains("image"))
					{
						return View("Profile picture must be an image type");
					}
				}
				var updateProfileInfo = _userHelper.UpdateProfile(applicationUser, User.Identity.Name);
				if (updateProfileInfo)
				{
					return View("Profile Information Updated Successfully");
				}
				else
				{
					return View("Error occured While updating information, try again.");
				}
			}
			catch (Exception exp)
			{
				throw exp;
			}

		}


        [HttpGet]
        public async Task<JsonResult> GetArreyListOfFaculties()
        {
            var faculties = _userHelper.GetArreyListOfFaculties().Result;
				
			if (faculties != null)
            {
				return Json(faculties);
            }
			return null;
        }
    }
}


 