namespace Emirate.Models
{
	public class BaseModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public DateTime DateCreated { get; set; }
		public bool Deleted { get; set; }

	}
}
