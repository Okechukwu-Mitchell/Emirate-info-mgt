using Emirate.DATABASE;
using Emirate.Enum;
using Emirate.Helper;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Xml.Linq;

namespace Emirate.Controllers
{
    [Authorize]
	public class AdminController : Controller
	{
		private readonly ApplicationDb _context;
        private readonly IAdminHelper _adminHelper;
        private readonly IUserHelper _userHelper;
		private readonly IDropdownHelper _dropdownHelper;
		private readonly IDescriptionHelper _descriptionHelper;

        public AdminController(ApplicationDb context, IUserHelper userHelper, IDropdownHelper dropdownHelper,  IDescriptionHelper descriptionHelper, IAdminHelper adminHelper)
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
        public IActionResult AdminDashBoard()
		{
           var jjj = User.Identity.Name;
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
            var factulty = _context.Faculty.Where(f => f.Name.ToLower() == dept.Faculty.ToLower() && !f.Deleted).FirstOrDefault();
            dept.FacultyId = factulty.Id;
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
            return Json(new { isError = true, msg = "Error Occured" });
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
            return Json(new { isError = true, msg = "Error Occured" });

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
