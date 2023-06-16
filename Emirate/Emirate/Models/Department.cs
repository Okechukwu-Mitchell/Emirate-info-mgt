using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emirate.Models
{
	public class Department:BaseModel
	{
        public int? FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public virtual Faculty? Faculty { get; set; }
    }

}
