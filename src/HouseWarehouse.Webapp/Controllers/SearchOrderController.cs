using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    public class SearchOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
