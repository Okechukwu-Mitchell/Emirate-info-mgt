using System.ComponentModel;

namespace Emirate.Enum
{
	public enum CourseProgress
	{
		[Description("For Completed Course")]
		Completed = 1,
		[Description("For Course in Progress")]
		InProgress = 2,

		[Description("For Carried Over Course")]
		CarryOver = 3,
	}
}