using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    [Authorize(Roles = "Admin, Member, User")]
    public class WishlistController : Controller
    {
        private readonly IWishlistApiClient _wishlistApiClient;

        public WishlistController(IWishlistApiClient wishlistApiClient)
        {
            _wishlistApiClient = wishlistApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;

            var request = new ProductLikeSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                IdMember = userId
            };
            var data = await _wishlistApiClient.Get(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string idProd)
        {
            var request = new ProductLikeModel();
            request.ProductId = idProd;

            if (!ModelState.IsValid)
                return View(request);
            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;
            request.MemberId = userId;

            var result = await _wishlistApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Đã thích sản phẩm";
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }
    }
}