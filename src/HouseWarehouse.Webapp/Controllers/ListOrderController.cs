using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    public class ListOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
