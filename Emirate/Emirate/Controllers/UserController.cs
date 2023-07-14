
using Emirate.DATABASE;
using Emirate.EmirateEnums;
using Emirate.Enum;
using Emirate.Helper;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace Emirate.Controllers
{
    public class UserController : Controller
	{
		private ApplicationDb _context;
		private readonly IUserHelper _userHelper;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IDropdownHelper _dropdownHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(ApplicationDb context,  IUserHelper userHelper, SignInManager<ApplicationUser> signInManager, IDropdownHelper dropdownHelper, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_userHelper = userHelper;
			_signInManager = signInManager;
            _dropdownHelper = dropdownHelper;
            _webHostEnvironment = webHostEnvironment;
        }
		public IActionResult Index()
		{
			return View();
		}

        [HttpGet]
        public async Task<IActionResult> MyCourses()
        {
            var username = User.Identity.Name;
            var coursess = _userHelper.MyCourses(username);
            if (coursess != null && coursess.Count() > 0)
            {
                return View(coursess);
            }
            return View();
        }





        [HttpGet]
         public IActionResult MyCoursesReg()
		 {
            var username = User.Identity.Name;

            var studentCourse = _userHelper.MyCoursesReg(username);

            if (studentCourse != null)
            {
                return View(studentCourse);
            }
            return View();
		 }

        public IActionResult DisplaySelectedCourses(string username, List<int> selectedCourseIds)
        {
            var selectedCourses = _userHelper.GetSelectedCourses(username, selectedCourseIds);
            return View("SelectedCourses", selectedCourses);
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
			var fees = _context.SchoolFees.Where(h => !h.Deleted && h.Active).Include(h => h.Faculty).Include(h => h.Level).OrderBy(a => a.Faculty).ThenBy(a => a.Level).ToList();
            if (fees.Count > 0)
            {
                var data = new SchoolFeesViewModel()
                {
                    SchoolFees = fees,
                };
                return View(data);
            }

			return View();
		}


        public async Task<IActionResult> PaySchoolFees()
        {
            var username = User.Identity.Name;
            var user = await _context.Applications.Where(c => c.UserName == username && !c.IsDeactivated)
                .Include(u => u.Department).Include(u => u.Level).Include(u => u.Department.Faculty).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            var schoolFees = _context.SchoolFees.Where(f => f.FacultyId == user.Department.FacultyId && f.Active).FirstOrDefault();
            if (schoolFees != null)
            {
                user.PaymentStatus = _userHelper.GetEnumDescription(schoolFees.PaymentStatus);
                var userDetails = new StudentSchoolFeesRecordViewModel()
                {

                    User = user,
                    SchoolFee = schoolFees

                };
                return View(userDetails);
            }
            return View();
           
        }

        //[HttpGet]
        //public async Task<IActionResult> SchoolFeesRecord()
        //{
        //    var username = User.Identity.Name;
        //    var user = await _context.Applications.Where(c => c.UserName == username && !c.IsDeactivated)
        //        .Include(u => u.Department).Include(u => u.Level).Include(u => u.Department.Faculty).FirstOrDefaultAsync();
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    var schoolFees = _context.SchoolFees.Where(f => f.FacultyId == user.Department.FacultyId && f.Active).FirstOrDefault();
        //    if (schoolFees != null)
        //    {
        //        user.PaymentStatus = _userHelper.GetEnumDescription(schoolFees.PaymentStatus);
        //        var userDetails = new StudentSchoolFeesRecordViewModel()
        //        {

        //            User = user,
        //            SchoolFee = schoolFees,
        //            Status = schoolFees.PaymentStatus,

        //        };
        //        return View(userDetails);
        //    }
        //    return View();
        //}


        [HttpGet]
        public IActionResult SchoolFeesRecord()
        {
            var currentLoggedInUser = User?.Identity?.Name;
            var student =_userHelper.MyStudentPaymentRecord(currentLoggedInUser);
            if (student != null)
            {
                return View(student);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult PaymentProof()
		{
			return View();
		}


        [HttpPost]
        public async Task<IActionResult> PaymentProof(IFormFile file)
        {
            var username = User.Identity.Name;
            var user = await _context.Applications.Where(c => c.UserName == username && !c.IsDeactivated)
                .Include(u => u.Department).Include(u => u.Level).Include(u => u.Department.Faculty).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            if (file == null || file.Length <= 0)
            {
                return RedirectToAction("PaymentProof");
            }
            var schoolFee = _context.SchoolFees.Where(d => d.Id != Guid.Empty && d.FacultyId == user.Department.FacultyId && d.LevelId == user.LevelId && !d.Deleted && d.Active).FirstOrDefault();
            if (schoolFee == null)
                return null;
            // Generate a unique file name
            var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
            var fileId = Guid.NewGuid().GetHashCode();


            // Get the path where the file will be saved
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

           

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
           
            var paymentEvidence = new StudentPayment
            {
                Username = user.UserName,		// Assuming you're using authentication and the user is logged in
                ApplicationUserId = user.Id,
                FilePath = filePath,
                Status = PaymentStatus.Pending,
                FileName = fileName,
                SchoolFeesId = schoolFee.Id,
                Active = true,
                Deleted = false,
                Amount = schoolFee.Amount,
                ApplicationUser = user,
                DateCreated = DateTime.Now,
                DatePaid = schoolFee.DatePaid,
                StudentName = user.FullName,
                DocumentId = fileId ,
                PaymentDocument = filePath,
                Name = user.FirstName
              
            };

            // Save the payment evidence to the database or perform any other required operations
            _context.StudentPayments.Add(paymentEvidence);
            _context.SaveChanges();


            return RedirectToAction("SchoolFeesRecord");
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


 