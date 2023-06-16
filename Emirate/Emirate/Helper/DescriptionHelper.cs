using Emirate.DATABASE;
using Emirate.Enum;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.Reflection;

namespace Emirate.Helper
{
    public class DescriptionHelper : IDescriptionHelper
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDb _context;

        public DescriptionHelper(ApplicationDb context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public DescriptionHelper()
        {

        }

        public string GetSemesterDescription(Semester value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                var des = attributes.First().Description;
                return des;
            }

            return value.ToString();
        }

        public bool EditFaculty(ModuleDropdownViewModel facultyDetails)
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

                        _context.Faculty.Update(faculty);
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
    }
}