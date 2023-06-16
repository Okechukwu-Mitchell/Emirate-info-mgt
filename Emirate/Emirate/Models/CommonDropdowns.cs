using Emirate.Enum;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Emirate.Models
{
	public class CommonDropdowns : BaseModel
	{
        [Display(Name = "Dropdown Key")]
        public int DropdownKey { get; set; }
	}
}
