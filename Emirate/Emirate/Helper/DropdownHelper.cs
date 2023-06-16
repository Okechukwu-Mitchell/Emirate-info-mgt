using Emirate.DATABASE;
using Emirate.Enum;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace Emirate.Helper
{
	public class DropdownHelper : IDropdownHelper
	{
		private readonly ApplicationDb _context;
        private readonly IDescriptionHelper _descriptionHelper;

        public DropdownHelper(ApplicationDb context, IDescriptionHelper descriptionHelper)
		{
			_context = context;
            _descriptionHelper = descriptionHelper;

        }
		public List<CommonDropdowns> DropdownOfLevels()
		{
			try
			{
				var common = new CommonDropdowns()
				{
					Id = 0,
					Name = "--Select--"
				};
				var ListOfLevels = _context.Levels.Where(s => s.Id > 0 && !s.Deleted).Include(f => f.Department).ToList();
               
				var drp = ListOfLevels.Select(s => new CommonDropdowns
				{
					Id = s.Id,
					Name = s.Name,
                    
				}).ToList();

				drp.Insert(0, common);
				return drp;
			}

			catch (Exception exp)
			{
				throw exp;
			}

		}

		public async Task<List<CommonDropdowns>> GetDropdownByKey(DropDownKey dropdownKey)
		{
			var common = new CommonDropdowns()
			{
				Id = 0,
				Name = "-- Select --"
			};
			var dropdowns = await _context.CommonDropdowns.Where(s => s.DropdownKey == (int)dropdownKey)
				.OrderBy(s => s.Name).ToListAsync();
			dropdowns.Insert(0, common);
			return dropdowns;
		}

        public List<Faculty> GetFacultyDropdown()
        {
            var common = new Faculty()
            {
                Id = 0,
                Name = "-- Select --"
            };
            var dropdowns = _context.Faculty.Where(s => s.Id > 0 && !s.Deleted)
                .OrderBy(s => s.Name).Select(d => new Faculty
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();
            dropdowns.Insert(0, common);
            return dropdowns;
        }
        public List<Department> GetDepartmentDropdown()
        {
            var common = new  Department()
            {
                Id = 0,
                Name = "-- Select --"
            };
            var dropdowns = _context.Departments.Where(s => s.Id > 0 && !s.Deleted).OrderBy(s => s.Name).ToList();
                //.OrderBy(s => s.Name).Select(d => new Department
                //{
                //	Id = d.Id,
                //	Name = d.Name
                //}).ToList();
            dropdowns.Insert(0, common);
            return dropdowns;
        }

        public List<ModuleDropdownViewModel> GetSemester()
        {
            var data = new List<ModuleDropdownViewModel>();
            var semester = ((Semester[])System.Enum.GetValues(typeof(Semester)))
				.Select(c => new ModuleDropdownViewModel() { Id = (int)c, Name = c.ToString() }).ToList();
            foreach (var item in semester)
            {
                var SemesterId = (Semester)item.Id;
                var description = _descriptionHelper.GetSemesterDescription(SemesterId);
                var sem = new ModuleDropdownViewModel()
                {
                    Name = item.Name,
                    Id = item.Id,
                    NameToString = item.Name + " " + description
                };
                data.Add(sem);

            }
            return data;
        }


        public async Task<List<CommonDropdowns>> GetLevelEnumsList(DropDownKey dropdownKey)
		{
			var common = new CommonDropdowns()
			{
				Id = 0,
				Name = "-- Select --"
			};
			var dropdowns = await _context.CommonDropdowns.Where(s => s.DropdownKey == (int)dropdownKey)
				.OrderBy(s => s.Name).ToListAsync();
			dropdowns.Insert(0, common);
			return dropdowns;
		}
	}
}
