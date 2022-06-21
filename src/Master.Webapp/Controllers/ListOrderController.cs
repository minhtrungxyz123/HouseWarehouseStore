using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    public class ListOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
