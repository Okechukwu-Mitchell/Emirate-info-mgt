using Microsoft.AspNetCore.Mvc;

namespace Emirate.Controllers
{
	public class EmirateController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

        public IActionResult Departments()
        {
            return View();
        }

        public IActionResult Admission()
        {
            return View();
        }
        public IActionResult Academics()
        {
            return View();
        }
        public IActionResult Research()
        {
            return View();
        }
        public IActionResult Library()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
    }
}
