using Emirate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Emirate.DATABASE
{
    public class ApplicationDb : IdentityDbContext	
	{
        public ApplicationDb(DbContextOptions<ApplicationDb> options):base(options)
		{

		}
        public DbSet<ApplicationUser> Applications { get; set; }
		public DbSet<StudentCourse> StudentCourses { get; set; }
		public DbSet<Courses> Courses { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Level> Levels { get; set; }
		public DbSet<CommonDropdowns> CommonDropdowns { get; set; }
		public DbSet<SchoolFee> SchoolFees { get; set; }
		public DbSet<StudentSchoolFeesRecord> StudentSchoolFeesRecords { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<StudentCourseLog> StudentCourseLogs { get; set; }
        public DbSet<StudentPayment> StudentPayments { get; set; }
  
    }

	
}
