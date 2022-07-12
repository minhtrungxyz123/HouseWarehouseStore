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

        public async Task<IActionResult> Index(string? idMember, string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ProductLikeSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                IdMember = idMember
            };
            var data = await _wishlistApiClient.Get(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }
    }
}