using Emirate.DATABASE;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
//using System.Data.Entity;
using System.Security.Policy;

namespace Emirate.Helper
{
    public class AdminHelper : IAdminHelper
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDb _context;
        private readonly IUserHelper _userHelper;
        public AdminHelper(ApplicationDb context, UserManager<ApplicationUser> userManager, IUserHelper userHelper)
        {
            _userManager = userManager;
            _context = context;
            _userHelper = userHelper;
        }
        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<ApplicationUser> FindByUserNameAsync(string username)
        {
            return await _userManager.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
        }
        public ApplicationUser FindUserByEmail(string email)
        {
            return _userManager.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string id)
        {
            return await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> AdminRegister(ApplicationUserViewModel model, string base64)
        {
            try
            {
                if (model != null)
                {
                    var adminUser = new ApplicationUser()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        Email = model.Email,
                        UserName = model.Email,
                        Password = model.Password,
                        ConfirmPassword = model.ConfirmPassword,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        CountryId = model.CountryId,
                        StateId = model.StateId,
                        Image = base64,
                        DateCreated = DateTime.Now,
                        IsDeactivated = false,
                        UserRole = model.Role,
                        IsAdmin = true,
                    };
                    var result = _userManager.CreateAsync(adminUser, model.Password).Result;
					if (result.Succeeded)
					{
						await _userManager.AddToRoleAsync(adminUser, model.Role);
						return adminUser;
					}
					
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteFaculty(FacultyViewModel facultyDetails)
        {
            try
            {
                if (facultyDetails != null)
                {
                    var faculty = _context.Faculty.Where(c => c.Id == facultyDetails.Id && !c.Deleted).FirstOrDefault();
                    if (faculty != null)
                    {
                        faculty.Name = facultyDetails.Name;
                        faculty.DateCreated = DateTime.Now;

                        _context.Faculty.Remove(faculty);
                        _context.SaveChanges();
                        return true;
                    };
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool EditDepartment(DepartmentViewModel deptDetails)
        {
            try
            {
                if (deptDetails != null)
                {
                    var dept = _context.Departments.Where(c => c.Id == deptDetails.Id && !c.Deleted).FirstOrDefault();
                    if (dept != null)
                    {
                        dept.Name = deptDetails.Name;
                        dept.DateCreated = DateTime.Now;

                        _context.Departments.Update(dept);
                        _context.SaveChanges();
                        return true;
                    };
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteDepartment(int id)
        {
            try
            {
                if (id > 0)
                {
                    var dept = _context.Departments.Where(c => c.Id == id).FirstOrDefault();
                    if (dept != null)
                    {
                        dept.Deleted = true;

                        _context.Departments.Update(dept);
                        _context.SaveChanges();
                        return true;
                    };
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool EditLevel(LevelViewModel levelDetails)
        {
            try
            {
                if (levelDetails != null)
                {
                    var levl = _context.Levels.Where(e => !e.Deleted && e.Id == levelDetails.Id).FirstOrDefault();
                    if (levl != null)
                       
                    {
                        levl.Name = levelDetails.Name;
                        levl.DateCreated = DateTime.Now;
                        levl.Semester = levelDetails.Semester;
                        levl.DepartmentId = Convert.ToInt32(levelDetails.Department);
                        levl.TotalCreditLoad = levelDetails.TotalCreditLoad;

                        _context.Levels.Update(levl);
                        _context.SaveChanges();
                        return true;
                    };
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteLevel(LevelViewModel levelDetails)
        {
            try
            {
                if (levelDetails != null)
                {
                    var levl = _context.Levels.Where(c => c.Id == levelDetails.Id && !c.Deleted).FirstOrDefault();
                    if (levl != null)
                    {
                        //levl.Name = levelDetails.Name;
                        //levl.DateCreated = DateTime.Now;

                        levl.Deleted = true;


                        //_context.Levels.Remove(levl);
                        _context.Update(levl);
                        _context.SaveChanges();
                        return true;
                    };
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<LevelViewModel> GetListOfLevels()
        {
            var listOfLevel = new List<LevelViewModel>();
            var levelss = _context.Levels.Where(r => r.Id > 0 && !r.Deleted).ToList();
            listOfLevel = levelss.Select(x => new LevelViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                DepartmentId = x.DepartmentId,  
                Semester = x.Semester,
                TotalCreditLoad = x.TotalCreditLoad,

            }).ToList();
            return listOfLevel;
        }


        public List<CourseViewModel> GetListOfCourse()
        {
            try
            {
                var listOfCourse = new List<CourseViewModel>();
                var courses = _context.Courses.Where(r => r.Id > 0 && !r.Deleted).ToList();
                listOfCourse = courses.Select(x => new CourseViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateCreated = x.DateCreated,  
                    CourseUnit = x.CourseUnit,
                    DepartmentId = x.DepartmentId,
                    Semester = x.Semester,
                    DepartmentName = x?.Department?.Name,
                    LevelId = x.LevelId,
                    LevelName = x?.Level?.Name,

                }).OrderBy(a => a.Name).ToList();
                if (listOfCourse.Any())
                {
                    return listOfCourse;
                } 
                return listOfCourse;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public bool Courses(CourseViewModel courseDetail)
        {
            try
            {
                if (courseDetail != null)
                {
                    var corses = new Courses()
                    {
                        Name = courseDetail.Name,
                        DateCreated = DateTime.Now,
                        Semester = courseDetail.Semester,
                        DepartmentId = courseDetail.DepartmentId,
                        CourseUnit = courseDetail.CourseUnit,
                        Level = courseDetail.Level,
                        LevelId = courseDetail.LevelId,
                    };
                    _context.Courses.Add(corses);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool EditCourses(CourseViewModel courseDetail)
        {
            try
            {
                if (courseDetail != null)
                {
                    var cus = _context.Courses.Where(c => c.Id == courseDetail.Id && !c.Deleted).FirstOrDefault();
                    if (cus != null)
                    {
                        cus.Name = courseDetail.Name;
                        cus.DateCreated = DateTime.Now;
                        cus.CourseUnit = courseDetail.CourseUnit;
                        cus.Level = courseDetail.Level;
                        cus.Semester = courseDetail.Semester;
                        cus.Department  = courseDetail.Department;

                        _context.Courses.Update(cus);
                        _context.SaveChanges();
                        return true;
                    };
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCourses(int id)
        {
            try
            {
                if (id > 0)
                {
                    var cours = _context.Courses.Where(c => c.Id == id).FirstOrDefault();
                    if (cours != null)
                    {
                        cours.Deleted = true;

                        _context.Courses.Remove(cours);
                        _context.SaveChanges();
                        return true;
                    };
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public bool SchoolFee(SchoolFeesViewModel payment)
        {
            try
            {
                if (payment != null)
                {
                    var level1Fee = new SchoolFee()
                    {
                        FacultyId = payment.FacultyId,
                        DateCreated = DateTime.Now,
                        Amount = payment.Amount1,
                        LevelId = payment.LevelId1,
                        PaymentStatus = payment.PaymentStatus,
                        Active = true,
                        Deleted = false,
                    };
                    _context.SchoolFees.Add(level1Fee);

                    var level2Fee = new SchoolFee()
                    {
                        FacultyId = payment.FacultyId,
                        DateCreated = DateTime.Now,
                        Amount = payment.Amount2,
                        LevelId = payment.LevelId2,
                        PaymentStatus = payment.PaymentStatus,
                        Active = true,
                        Deleted = false
                         
                    };
                    _context.SchoolFees.Add(level2Fee);
                   

                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        //public List<ApplicationUserViewModel> GetListOfStudent()
        //{
        //    try
        //    {
        //        var newApplication = new List<ApplicationUserViewModel>();
        //        var application = _context.Applications.Where(y => y.Id != null && !y.IsDeactivated).ToList();
        //        if (application != null)
        //        {
        //            foreach (var item in application)
        //            {
        //                var student = new ApplicationUserViewModel();

        //                student.Id = item.Id;   
        //                student.Address = item.Address; 
        //                student.PhoneNumber = item.PhoneNumber;
        //                student.UserName = item.UserName;
        //                student.Role = item.UserRole;
        //                student.StateId = item.StateId;
        //                student.DepartmentId = item.DepartmentId;
        //                student.Departments = item.Department;
        //                student.LastName = item.LastName;
        //                student.Levels = item.Level;
        //                student.LevelId = item.LevelId; 
        //                student.MiddleName = item.MiddleName;
        //                student.FirstName = item.FirstName;

        //                newApplication.Add(student);
        //            }
        //            return newApplication;
        //        }
        //        return newApplication;
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }

        //}

        public List<StudentPaymentViewModel> SchoolMoney()
        {
            try
            {
                var allSchoolViewModel = new List<StudentPaymentViewModel>();
                var schoolFees = _context.StudentPayments.Where(g => g.Id != 0 && !g.Deleted && g.Active)
                .Include(g => g.ApplicationUser).Include(u => u.SchoolFees).Include(j => j.ApplicationUser.Level.Department.Faculty)
                .ToList();
                if (schoolFees != null && schoolFees.Count() > 0)
                {
                    foreach (var school in schoolFees)
                    {
                        var newSchool = new StudentPaymentViewModel()
                        {
                           Id = school.Id,
                           Amount = school.Amount,
                           ApplicationUser = school.ApplicationUser,
                           SchoolFeesId = school.SchoolFeesId,
                           ApplicationUserId = school.ApplicationUserId,
                           DatePaid = school.DatePaid,
                           SchoolFees = school.SchoolFees,
                           DocumentId = school.DocumentId,
                           FileName = school.FileName,  
                           FilePath = school.FilePath,
                           PaymentDate = school.PaymentDate,
                           PaymentDocument = school.FileName,
                           StudentName = school.StudentName,
                           Status = school.Status,
                           ReferenceNumber = school.ReferenceNumber,
                           Username = school.Username,

                        };
                        allSchoolViewModel.Add(newSchool);
                    }
                    return allSchoolViewModel;
                }
                return allSchoolViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public StudentPayment ApproveDocument(int id)
        {
            try
            {
                if (id > 0)
                {
                    var app = new StudentPaymentViewModel();
                    var student = _context.StudentPayments.Where(h => h.Id == id && h.Active && !h.Deleted && h.Status == EmirateEnums.PaymentStatus.Pending)
                    .Include(u => u.SchoolFees).Include(s => s.ApplicationUser).FirstOrDefault();
                    if (student != null)
                    {
                       
                        student.Status = EmirateEnums.PaymentStatus.Approved;
                       
                        _context.StudentPayments.Update(student); 
                        _context.SaveChanges();
                        return student;
                    };
                }
                return null;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }




        public StudentPayment DeclineDocument(int id)
        {
            try
            {
                if (id > 0)
                {
                    var dec = new StudentPaymentViewModel();
                    var student = _context.StudentPayments.Where(h => h.Id == id && h.Active && !h.Deleted && h.Status == EmirateEnums.PaymentStatus.Pending)
                    .Include(u => u.SchoolFees).Include(s => s.ApplicationUser).FirstOrDefault();
                    if (student != null)
                    {
                       
                        student.Status = EmirateEnums.PaymentStatus.Declined;

                        _context.StudentPayments.Update(student);
                        _context.SaveChanges();
                        return student;
                    };
                }
                return null;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }








    }
}
