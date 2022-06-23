using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
