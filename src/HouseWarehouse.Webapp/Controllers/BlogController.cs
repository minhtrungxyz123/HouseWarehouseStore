using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
