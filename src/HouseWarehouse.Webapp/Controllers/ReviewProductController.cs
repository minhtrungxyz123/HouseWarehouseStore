using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    public class ReviewProductController : Controller
    {
        private readonly ICommentApiClient _commentApiClient;

        public ReviewProductController(ICommentApiClient commentApiClient)
        {
            _commentApiClient = commentApiClient;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return ViewComponent("ReviewProduct");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Email").Value;
            request.CustomerName = userId;
            request.Profession = "Khách hàng";

            var result = await _commentApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }
    }
}