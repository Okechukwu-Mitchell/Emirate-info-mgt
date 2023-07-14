using Microsoft.AspNetCore.Mvc;

namespace Emirate.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Staff()
        {
            return View();
        }
    }
}
