using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    public class ProductLikeController : Controller
    {
        #region Fields

        private readonly IProductLikeApiClient _productLikeApiClient;

        public ProductLikeController(IProductLikeApiClient productLikeApiClient)
        {
            _productLikeApiClient = productLikeApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ProductLikeSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _productLikeApiClient.Get(request);

            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        #endregion List
    }
}