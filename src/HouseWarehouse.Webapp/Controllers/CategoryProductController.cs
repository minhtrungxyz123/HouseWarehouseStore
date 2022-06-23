using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    public class CategoryProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
