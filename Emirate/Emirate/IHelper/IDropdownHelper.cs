using Emirate.Enum;
using Emirate.Models;
using Emirate.ViewModels;

namespace Emirate.IHelper
{
	public interface IDropdownHelper
	{
		//List<CommonDropdowns> DropdownOfLevels();
		Task<List<CommonDropdowns>> GetDropdownByKey(DropDownKey dropdownKey);
		Task<List<CommonDropdowns>> GetLevelEnumsList(DropDownKey dropdownKey);
		List<Faculty> GetFacultyDropdown();
		List<Department> GetDepartmentDropdown();
		List<ModuleDropdownViewModel> GetSemester();
		List<Level> DropdownOfLevels();
    }
}
