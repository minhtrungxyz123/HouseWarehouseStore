using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly ICommentApiClient _commentApiClient;

        public CategoryController(ICategoryApiClient categoryApiClient,
            ICommentApiClient commentApiClient)
        {
            _categoryApiClient = categoryApiClient;
            _commentApiClient = commentApiClient;
        }

        public async Task<IActionResult> Index(string id, string keyword, int pageIndex = 1, int pageSize = 9)
        {
            var request = new ProductCategorySearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryId = id
            };
            var data = await _categoryApiClient.Get(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }
    }
}