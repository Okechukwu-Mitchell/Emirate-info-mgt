using Emirate.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Emirate.Models
{
	public class Level:BaseModel
	{
		public int TotalCreditLoad { get; set; }
        public Semester Semester { get; set; }

        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }

       
    }
}
