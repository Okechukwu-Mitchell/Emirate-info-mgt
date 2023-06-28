using Emirate.Enum;
using Emirate.Helper;
using Emirate.Models;
using Emirate.ViewModels;

namespace Emirate.IHelper
{
    public interface IDescriptionHelper
    {
        string GetSemesterDescription(Semester value);
        bool EditFaculty(ModuleDropdownViewModel details);
        ApplicationUser EditUser(ApplicationUserViewModel userDetails);
        bool DeleteUser(string id);
    }
}
