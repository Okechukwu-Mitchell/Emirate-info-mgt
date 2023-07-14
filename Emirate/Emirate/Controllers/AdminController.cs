
using Emirate.DATABASE;
using Emirate.EmirateEnums;
using Emirate.Enum;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;



namespace Emirate.Controllers
{
    
	public class AdminController : Controller
	{
		private readonly ApplicationDb _context;
        private readonly IAdminHelper _adminHelper;
        private readonly IUserHelper _userHelper;
		private readonly IDropdownHelper _dropdownHelper;
		private readonly IDescriptionHelper _descriptionHelper;
        private readonly IEmailHelper _emailHelper;
        private readonly DbContextOptions<ApplicationDb> options;
        public AdminController(ApplicationDb context, IUserHelper userHelper, IDropdownHelper dropdownHelper,  IDescriptionHelper descriptionHelper,
            IAdminHelper adminHelper)
        {
			_context = context;
			_userHelper = userHelper;
            _dropdownHelper = dropdownHelper;
            _descriptionHelper = descriptionHelper;
            _adminHelper = adminHelper;
        }
        public IActionResult Index()
		{
			return View();
		}
        public IActionResult testpage()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashBoard()
		{
           var fac = _context.Faculty.ToList();
           var dept = _context.Departments.ToList();
           var user = _context.Applications.ToList();

			var dashboard = new DashboardViewModel
			{
				faculty = fac,
				department = dept,
				applicationUser = user,
			};

			return View(dashboard);
		}




        [HttpGet]
        public IActionResult SchoolFee()
        {
            ViewBag.Level = _dropdownHelper.DropdownOfLevels();
            ViewBag.Faculty = _dropdownHelper.GetFacultyDropdown();

            var schoolMoney = _adminHelper.SchoolMoney();
            if (schoolMoney != null && schoolMoney.Count() > 0)
            {
                return View(schoolMoney);
            }
            return View();
        }








        //[HttpGet]
        //public IActionResult SchoolFee()
        //{
        //    ViewBag.Level = _dropdownHelper.DropdownOfLevels();
        //    ViewBag.Faculty = _dropdownHelper.GetFacultyDropdown();


        //    var user = _context.Applications.Where(c => c.UserName != null && !c.IsDeactivated && !c.IsAdmin)
        //        .Include(u => u.Department).Include(u => u.Level).Include(u => u.Department.Faculty)
        //        .Select(s => new PayViewModel
        //        {
        //            UserId = s.Id,
        //            UserName = s.UserName,
        //            LastName = s.LastName,
        //            MiddleName = s.MiddleName,
        //            FirstName = s.FirstName,        
        //            RegNumber = s.RegNumber,
        //            DatePaid = DateTime.Now,
        //            //Faculty = s.Department.Faculty,
        //            //Status = PaymentStatus.Pending,

        //            //SchoolFees = _context.SchoolFees
        //            //.Where(x => x.Faculty == s.Department.Faculty && x.Level == s.Level)
        //            //.Select(x => x.Amount.ToString())
        //            //.FirstOrDefault(),

        //            SchoolFees = _context.SchoolFees.FirstOrDefault(x => x.FacultyId == s.Department.FacultyId && x.Level == s.Level).Amount.ToString(),
        //        }).ToList();

        //    return View(user);

        //}




        public IActionResult Documents()
        {
            var pendingDocuments = _context.StudentPayments.Where(d => d.Status == PaymentStatus.Pending).ToList();
            return View(pendingDocuments);

        }

        
        //public IActionResult ApproveDocuments(int documentId)
        //{
        //    var document = _context.StudentPayments.Find(documentId);
        //    if (document != null && document.Status == DocumentStatus.Pending)
        //    {
        //        document.Status = DocumentStatus.Approved;
        //        _context.SaveChanges();
        //    }
        //    return RedirectToAction("SchoolFee");

        //}
        public IActionResult ApproveDocument(int id)
        {
            var student = _adminHelper.ApproveDocument(id);
            if (student != null)
            {
                return RedirectToAction("SchoolFee");
            }
            return View();

        }

        
        public IActionResult DeclineDocument(int id)
        {
            var student = _adminHelper.DeclineDocument(id);
            if (student != null)
            {
                return RedirectToAction("SchoolFee");
            }
            return View();
            
        }


        //public IActionResult UpdatePaymentStatus(int studentId, PaymentStatus newStatus)
        //{
        //    // Retrieve the student record from the database based on the student ID
        //    var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);

        //    if (student == null)
        //    {
        //        // Handle the case where the student is not found
        //        return NotFound();
        //    }

        //    // Update the student's payment status with the new value
        //    student.PaymentStatus = newStatus;

        //    // Save the changes to the database
        //    _context.SaveChanges();

        //    // Redirect or return a response indicating the successful status update
        //    return RedirectToAction("Index", "Student"); // Redirect to the student list or any other appropriate action
        //}


        [HttpPost]
        public JsonResult SchoolFee(string payment)
        {
            ViewBag.Level = _dropdownHelper.DropdownOfLevels();
            ViewBag.Faculty = _dropdownHelper.GetFacultyDropdown();

            if (payment != null)
            {
                var fees = JsonConvert.DeserializeObject<SchoolFeesViewModel>(payment);
                if (fees != null)
                {
                    var result = _adminHelper.SchoolFee(fees);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Created Successfully" });
                    }
                }
                return Json(new { isError = true, msg = "Unable to Update" });
            }
            return Json(new { isError = true, msg = "Error Occurred" });
        }

        public IActionResult Students()
		{
            ViewBag.Level = _dropdownHelper.DropdownOfLevels();
            ViewBag.Gender = _dropdownHelper.GetDropdownByKey(DropDownKey.Gender).Result;
            ViewBag.Department = _dropdownHelper.GetDepartmentDropdown();

            var user = _context.Applications.Where(c => !c.IsDeactivated && !c.IsAdmin).Include(d => d.Department).Include(b => b.Level).Include(g => g.Gender).OrderBy(p => p.FirstName).ToList();
            if (user.Count > 0)
            {
                var data = new ApplicationUserViewModel()
                {
                    Applications = user,
                };
                return View(data);
            }
            return View();
		}


        [HttpGet]
        public IActionResult Faculty()
        {
            var faculty = _context.Faculty.Where(f => !f.Deleted).OrderBy(b => b.Name).ToList();
            if (faculty.Count > 0)
            {
                var data = new FacultyViewModel()
                {
                    Faculties = faculty,
                };
                return View(data);

            }
            return View();
        }


        [HttpPost]
        public IActionResult Faculty(string name)
        {
            var check = _context.Faculty.Where(f => f.Name.ToLower() == name.ToLower() && !f.Deleted).FirstOrDefault();
            if (check != null)
            {
                return Json(new { isError = true, msg = "Name Already Exists" });
            }

            if (ModelState.IsValid)
            {
                var model = new Faculty()
                {
                    DateCreated = DateTime.Now,
                    Deleted = false,
                    Name = name,
                };

                _context.Faculty.Add(model);
                _context.SaveChanges();
                return Json(new { isError = false, msg = "Created Successfully" });
            }
            return Json(new { isError = true, msg = "Failed" });
        }


        [HttpGet]
        public JsonResult EditFacultyByID(int EditFacultyId)
        {
            if (EditFacultyId != 0)
            {
                var faculty = _context.Faculty.Where(c => c.Id == EditFacultyId).FirstOrDefault();
                if (faculty != null)
                {
                    return Json(faculty);
                }
            }
            return null;

        }


        [HttpPost]
        public JsonResult EditFaculty(string details)
        {

            if (details != null)
            {
                var faculty = JsonConvert.DeserializeObject<ModuleDropdownViewModel>(details);
                if (faculty != null)
                {
                    var result = _descriptionHelper.EditFaculty(faculty);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Updated Successfully" });
                    }
                }
                return Json(new { isError = true, msg = "Unable to Update" });
            }
            return Json(new { isError = true, msg = "Error Occured" });

        }



        [HttpGet]
        public JsonResult DeleteFacultyById(int DeleteFacultyId)
        {
            if (DeleteFacultyId != 0)
            {
                var faculty = _context.Faculty.Where(b => b.Id == DeleteFacultyId).FirstOrDefault();
                if (faculty != null)
                {
                    return Json(faculty);
                }
            }
            return null;
        }



        [HttpPost]
        public JsonResult DeleteFaculty(string details)
        {
            if (details != null)
            {
                var faculty = JsonConvert.DeserializeObject<FacultyViewModel>(details);
                if (faculty != null)
                {
                    var result = _adminHelper.DeleteFaculty(faculty);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Deleted Successfully" });
                    }
                }
                return Json(new { isError = true, msg = "Unable to Delete" });
            }
            return Json(new { isError = true, msg = "Error Occurred" });
        }



        [HttpGet]
        public IActionResult Departments()
		{
            ViewBag.Faculty = _dropdownHelper.GetFacultyDropdown();

            var dept = _context.Departments.Where(f => f.Id > 0 && !f.Deleted).OrderBy(a => a.Name).ToList();
            if (dept.Any())
            {
                var data = new DepartmentViewModel()
                {
                    Departments = dept,
                };
                return View(data);
            }

            return View();
		}

        [HttpPost]
        public IActionResult Departments(string detail)
		{
            var dept = JsonConvert.DeserializeObject<DepartmentViewModel>(detail);
            //var factulty = _context.Faculty.Where(f => f.Name.ToLower() == dept.Faculty.ToLower() && !f.Deleted).FirstOrDefault();
            //dept.FacultyId = factulty.Id;
            var check = _context.Departments.Where(f => f.Name.ToLower() == dept.Name.ToLower()).FirstOrDefault();
            if (check != null) {
                if (check.Deleted)
                {
                    check.Deleted = false;
                    _context.Update(check);
                    _context.SaveChanges();

                    return Json(new { isError = false, msg = "Recalled Successfully" });
                }
                return Json(new { isError = true, msg = "Name Already Exists" });
            } 

            if (ModelState.IsValid)
            {
                var model = new Department()
                {
                    DateCreated = DateTime.Now,
                    Deleted = false,
                    Name = dept?.Name,
                    FacultyId = dept.FacultyId
                };
                _context.Departments.Add(model);
                _context.SaveChanges();
                return Json(new { isError = false, msg = "Created Successfully" });
            }
            return Json(new { isError = true, msg = "Failed" });
           
		}


        [HttpGet]
        public JsonResult EditDepartmentByID(int EditDepartmentId)
        {
            if (EditDepartmentId != 0)
            {
                var dept = _context.Departments.Where(c => c.Id == EditDepartmentId).FirstOrDefault();
                if (dept != null)
                {
                    return Json(dept);
                }
            }
            return null;

        }


        [HttpPost]
        public JsonResult EditDepartment(string details)
        {

            if (details != null)
            {
                var dept = JsonConvert.DeserializeObject<DepartmentViewModel>(details);
                if (dept != null)
                {
                    var result = _adminHelper.EditDepartment(dept);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Updated Successfully" });
                    }
                }
                return Json(new { isError = true, msg = "Unable to Update" });
            }
            return Json(new { isError = true, msg = "Error Occured" });

        }

         [HttpGet]
        public JsonResult DeleteDepartmentByID(int DeleteDepartmentId)
        {
            if (DeleteDepartmentId != 0)
            {
                var dept = _context.Departments.Where(c => c.Id == DeleteDepartmentId).FirstOrDefault();
                if (dept != null)
                {
                    return Json(dept);
                }
            }
            return null;

        }


        [HttpPost]
        public JsonResult DeleteDepartment(int id)
        {

            if (id > 0)
            {
                var result = _adminHelper.DeleteDepartment(id);
                if (result)
                {
                    return Json(new { isError = false, msg = "Deleted Successfully" });
                }
            }
            return Json(new { isError = true, msg = "Error Occured" });

        }

        [HttpGet]
        public IActionResult Level()
        {
            ViewBag.Department = _dropdownHelper.GetDepartmentDropdown();
            ViewBag.Semester = _dropdownHelper.GetSemester();

            var level = _context.Levels.Where(p => !p.Deleted).Include(d => d.Department)
                .Select(f => new LevelViewModel()
                {   
                   Id = f.Id,
                   TotalCreditLoad = f.TotalCreditLoad,
                   Name = f.Name,
                   Semester = f.Semester,
                   DepartmentId = f.DepartmentId,
                   Department = f.Department.Name,
                   DateCreated = f.DateCreated,
                   Deleted = f.Deleted,

                }).OrderBy(a => a.Department).ToList();
            return View(level);
        }


        [HttpPost]
        public IActionResult Level(string details)
        {
            var level = JsonConvert.DeserializeObject<LevelViewModel>(details);
            var check = _context.Levels.Where(f => f.Name == level.Name && !f.Deleted && f.DepartmentId == Convert.ToInt32(level.Department) && f.Semester == level.Semester).FirstOrDefault();
            if (check != null)
            {
                return Json(new { isError = true, msg = "Name Already Exists" });
            }
            if (ModelState.IsValid)
            {
                var model = new Level()
                {
                    Name = level?.Name,
                    DateCreated = DateTime.Now,
                    Deleted = false,
                    DepartmentId = Convert.ToInt32( level?.Department),
                    Semester = level.Semester,
                    TotalCreditLoad = level.TotalCreditLoad,
                   
                };

                _context.Levels.Add(model);
                _context.SaveChanges();
                return Json(new { isError = false, msg = "Added Successfully" });
            }
            return Json(new { isError = true, msg = "Unable to Add" });
        }

        [HttpGet]
        public JsonResult EditLevelByID(int EditLevelId)
        {
            if (EditLevelId != 0)
            {
                var levl = _context.Levels.Where(c => c.Id == EditLevelId).FirstOrDefault();
                if (levl != null)
                {
                    return Json(levl);
                }
            }
            return null;

        }


        [HttpPost]
        public JsonResult EditLevel(string details)
        {

            if (details != null)
            {
                var levl = JsonConvert.DeserializeObject<LevelViewModel>(details);
                if (levl != null)
                {
                    var result = _adminHelper.EditLevel(levl);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Updated Successfully" });
                    }
                }
                return Json(new { isError = true, msg = "Unable to Update" });
            }
            return Json(new { isError = true, msg = "Error Occured" });

        }


        [HttpGet]
        public JsonResult DeleteLevelByID(int DeleteLevelId)
        {
            if (DeleteLevelId != 0)
            {
                var levl = _context.Levels.Where(c => c.Id == DeleteLevelId).FirstOrDefault();
                if (levl != null)
                {
                    return Json(levl);
                }
            }
            return null;

        }


        [HttpPost]
        public JsonResult DeleteLevel(string details)
        {

            if (details != null)
            {
                var levl = JsonConvert.DeserializeObject<LevelViewModel>(details);
                if (levl != null)
                {
                    var result = _adminHelper.DeleteLevel(levl);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Deleted Successfully" });
                    }
                }
                return Json(new { isError = true, msg = "Unable to Deleted" });
            }
            return Json(new { isError = true, msg = "Error Occured" });

        }


        //GET
        [HttpGet]
		public IActionResult Courses()
		{
		    ViewBag.Gender = _dropdownHelper.GetDropdownByKey(DropDownKey.Gender).Result;
            ViewBag.Level = _dropdownHelper.DropdownOfLevels();
            ViewBag.Department = _dropdownHelper.GetDepartmentDropdown();
		    ViewBag.Semester = _dropdownHelper.GetSemester();
            var courses = _adminHelper.GetListOfCourse();
            if (courses.Any())
            {
                return View(courses);
            }
            return View(courses);
        }


        //POST
        //[HttpPost]
        //public IActionResult Courses(Courses courseDetail)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Courses.Add(obj);
        //            _context.SaveChanges();
        //            return RedirectToAction("Courses");
        //        }
        //        return View();
        //    }

        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}


        //POST
        [HttpPost]
        public JsonResult Courses(string courseDetail)
        {
            if (courseDetail != null)
            {
                var cours = JsonConvert.DeserializeObject<CourseViewModel>(courseDetail);

                var check = _context.Courses.Where(f => f.Name == cours.Name && !f.Deleted && f.DepartmentId == cours.DepartmentId &&
                   f.LevelId == cours.LevelId).FirstOrDefault();
                if (check != null)
                {
                    return Json(new { isError = true, msg = "Name Already Exists" });
                }

                if (cours != null)
                {
                    var result = _adminHelper.Courses(cours);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Added Successfully" });
                    }
                  return Json(new { isError = true, msg = "Name Already Exists" });
                }
                return Json(new { isError = true, msg = "Unable to Add" });
            }
            return Json(new { isError = true, msg = "Error Occurred" });
        }



        public JsonResult EditCoursesByID(int courseId)
        {
            if (courseId != 0)
            {
                var course = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
                if (course != null)
                {
                    return Json(new {isError = false, data = course });
                }
            }
            return Json(new { isError = true}); 

        }



        [HttpPost]
        public JsonResult EditCourses(string details)
        {

            if (details != null)
            {
                var cus = JsonConvert.DeserializeObject<CourseViewModel>(details);
                if (cus != null)
                {
                    var result = _adminHelper.EditCourses(cus);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Updated Successfully" });
                    }
                }
                return Json(new { isError = true, msg = "Unable to Update" });
            }
            return Json(new { isError = true, msg = "Error Occurred" });

        }



        [HttpGet]
        public JsonResult DeleteCoursesByID(int DeleteCoursesId)
        {
            if (DeleteCoursesId != 0)
            {
                var dept = _context.Courses.Where(c => c.Id == DeleteCoursesId).FirstOrDefault();
                if (dept != null)
                {
                    return Json(dept);
                }
            }
            return null;

        }


        [HttpPost]
        public JsonResult DeleteCourses(int id)
        {

            if (id > 0)
            {
                var result = _adminHelper.DeleteCourses(id);
                if (result)
                {
                    return Json(new { isError = false, msg = "Deleted Successfully" });
                }
            }
            return Json(new { isError = true, msg = "Error Occured" });

        }



        //[HttpGet]
        //public IActionResult EditFaculty(int? id)
        //{

        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var obj = _context.Faculty.Where(a => a.Id == id && !a.Deleted).Select(f => new FacultyViewModel()
        //    {
        //        Id = f.Id,
        //        Name = f.Name,
        //    }).FirstOrDefault();
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(obj);
        //}

    }
}
