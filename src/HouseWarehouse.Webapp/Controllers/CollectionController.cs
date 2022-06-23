using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
