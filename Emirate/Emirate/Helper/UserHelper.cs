
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.Reflection;
using System;
using Emirate.DATABASE;
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
                    ConfirmPassword = applicationUserViewModel.Password,
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


        public string GetEnumDescription(System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

          
            if (fi == null)
            {
                return value.ToString(); // or handle the situation in an appropriate way
            }

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                var des = attributes.First().Description;
                return des;
            }

            return value.ToString();
        }

        public List<StudentCourseViewModel> GetListOfCourse()
        {
            try
            {
                var listOfCourse = new List<StudentCourseViewModel>();
                var courses = _context.Courses.Where(r => r.Id > 0 && !r.Deleted).ToList();
                listOfCourse = courses.Select(x => new StudentCourseViewModel()
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

        //public string GetEnumDescription(System.Enum value)
        //{
        //    FieldInfo fi = value.GetType().GetField(value.ToString());

        //    DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        //    if (attributes != null && attributes.Length > 0)
        //    {
        //        return attributes[0].Description;
        //    }

        //    return value.ToString();
        //}




        //public string GetEnumDescription(System.Enum value)
        //{
        //    if (value == null)
        //    {
        //        // Handle null value case
        //        return string.Empty;
        //    }

        //    FieldInfo fi = value.GetType().GetField(value.ToString());

        //    if (fi == null)
        //    {
        //        // Handle missing field information case
        //        return value.ToString();
        //    }

        //    DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        //    if (attributes != null && attributes.Length > 0)
        //    {
        //        var des = attributes[0].Description;
        //        return des;
        //    }

        //    return value.ToString();
        //}


        //public string GetDescription(System.Enum value)
        //{
        //    Type enumType = value.GetType();
        //    MemberInfo[] memberInfo = enumType.GetMember(value.ToString());
        //    if (memberInfo.Length > 0)
        //    {
        //        object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        //        if (attributes.Length > 0)
        //        {
        //            return ((DescriptionAttribute)attributes[0]).Description;
        //        }
        //    }
        //    return value.ToString();
        //}

        public List<StudentCourseViewModel> MyCourses( string username)
        {
            try
            {
                var user = FindUserByEmail(username);
                
                var student = new List<StudentCourseViewModel>();
                var courses = _context.Courses.Where(q => q.Id != 0 && q.DepartmentId == user.DepartmentId && q.LevelId == user.LevelId && !q.Deleted).Include(h => h.Level)
                .Include(j => j.Department).Select(x => new StudentCourseViewModel(){ 

                    CourseName = x.Name,
                    Department = x.Department,
                    Semester = x.Semester,
                    DateCreated= x.DateCreated,
                    DepartmentId= x.DepartmentId,
                    Name = x.Name,
                    Id = x.Id,
                    LevelId= x.LevelId,
                    Level= x.Level,
                    CourseUnit = x.CourseUnit,
                    StudentName = user.FirstName,
                    StudentFullName = user.FullName,
                    StudentId = user.Id

                }).ToList();
                if (courses != null && courses.Count() > 0)
                {
                    return courses;
                }
                return null;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }





        public List<StudentCourseViewModel> MyCoursesReg(string username)
        {
            try
            {
                var user = FindUserByEmail(username);

                var student = new List<StudentCourseViewModel>();
                var selectedCourses = _context.Courses.Where(q => q.Id != 0 && q.DepartmentId == user.DepartmentId && q.LevelId == user.LevelId && !q.Deleted).Include(h => h.Level)
                .Include(j => j.Department).Select(x => new StudentCourseViewModel()
                {

                    CourseName = x.Name,
                    Department = x.Department,
                    Semester = x.Semester,
                    DateCreated = x.DateCreated,
                    DepartmentId = x.DepartmentId,
                    Name = x.Name,
                    Id = x.Id,
                    LevelId = x.LevelId,
                    Level = x.Level,
                    CourseUnit = x.CourseUnit,
                    StudentName = user.FirstName,
                    StudentFullName = user.FullName,
                    StudentId = user.Id

                }).ToList();
                if (selectedCourses != null && selectedCourses.Count() > 0)
                {
                    return selectedCourses;
                }
                return null;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }





        public List<StudentCourseViewModel> GetSelectedCourses(string username, List<int> selectedCourseIds)
        {
            try
            {
                var user = FindUserByEmail(username);

                var selectedCourses = _context.Courses
                    .Where(q => selectedCourseIds.Contains(q.Id) && q.DepartmentId == user.DepartmentId && !q.Deleted)
                    .Include(h => h.Level)
                    .Include(j => j.Department)
                    .Select(x => new StudentCourseViewModel()
                    {
                        CourseName = x.Name,
                        Department = x.Department,
                        Semester = x.Semester,
                        DateCreated = x.DateCreated,
                        DepartmentId = x.DepartmentId,
                        Name = x.Name,
                        Id = x.Id,
                        LevelId = x.LevelId,
                        Level = x.Level,
                        CourseUnit = x.CourseUnit,
                        StudentName = user.FirstName,
                        StudentFullName = user.FullName,
                        StudentId = user.Id
                    })
                    .ToList();

                return selectedCourses;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }




        public StudentPaymentViewModel MyStudentPaymentRecord(string currentLoggedInUser)
        {
            try
            {
                var student = new StudentCourseViewModel();
                var selectedCourses = _context.StudentPayments.Where(q => q.Id != 0 && q.Active && !q.Deleted && q.Username == currentLoggedInUser)
                .Include(y => y.SchoolFees).Include(u => u.ApplicationUser).Select(g => new StudentPaymentViewModel()
                {

                    StudentName = g.ApplicationUser.FullName,
                    SchoolFees = g.SchoolFees,
                    Status = g.Status,
                    Amount = g.Amount,
                    DatePaid = g.DatePaid,
                    SchoolFeesId = g.SchoolFeesId,
                    Username = g.Username,
                    RegNumber = g.ApplicationUser.RegNumber,
                    Id = g.Id,
                    Department = g.ApplicationUser.Department,
                    Level = g.ApplicationUser.Level,
                    Faculty = g.ApplicationUser.Department.Faculty,

                }).FirstOrDefault();
                if (selectedCourses != null)
                {
                    return selectedCourses;
                }
                return null;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        //public StudentPaymentViewModel MyStudentPaymentRecord(string currentLoggedInUser)
        //{
        //    try
        //    {
        //        var studentModel = new List<StudentPaymentViewModel>();
        //        var selectedCourses = _context.StudentPayments.Where(q => q.Id != 0 && q.Active && !q.Deleted && q.Username == currentLoggedInUser)
        //        .Include(y => y.SchoolFees).Include(u => u.ApplicationUser).ToList();
        //        if (selectedCourses != null)
        //        {
        //            foreach (var item in selectedCourses)
        //            {
        //                var application = _context.Applications.Where(j => j.Id == item.ApplicationUserId).FirstOrDefault();
        //                if (application != null)
        //                {
        //                    var students = new StudentPaymentViewModel()
        //                    {
        //                        ApplicationUserId = item.ApplicationUserId,
        //                        StudentName = item.StudentName,
        //                        RegNumber = item.ApplicationUser.RegNumber,
        //                        Level = item.ApplicationUser.Level,
        //                        Department = item.ApplicationUser.Department,
        //                        Faculty = item.ApplicationUser.Department.Faculty,
        //                        Amount = item.Amount,
        //                        Status = item.Status,
        //                        DatePaid = item.DatePaid,
        //                        SchoolFees = item.SchoolFees,
        //                        SchoolFeesId = item.SchoolFeesId,
        //                        Username = item.Username,
        //                    };
        //                    studentModel.Add(students);
        //                }
        //            }
        //            return studentModel;
        //        }
        //        return studentModel;
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}





    }
}
