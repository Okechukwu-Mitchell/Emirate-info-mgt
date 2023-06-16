using Emirate.Enum;
using Emirate.Helper;
using Emirate.ViewModels;

namespace Emirate.IHelper
{
    public interface IDescriptionHelper 
    {
        string GetSemesterDescription(Semester value);
        bool EditFaculty(ModuleDropdownViewModel details);
    }
}
