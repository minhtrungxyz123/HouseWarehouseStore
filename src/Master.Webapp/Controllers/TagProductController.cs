using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TagProductController : Controller
    {
        #region Fields

        private readonly ITagProductApiClient _tagProductApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly ITagApiClient _tagApiClient;

        public TagProductController(ITagProductApiClient tagProductApiClient,
            IProductApiClient productApiClient,
            ITagApiClient tagApiClient)
        {
            _tagProductApiClient = tagProductApiClient;
            _productApiClient = productApiClient;
            _tagApiClient = tagApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new TagProductSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _tagProductApiClient.Get(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        #endregion List

        #region Method

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return ViewComponent("CreateTagProduct");
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagProductModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _tagProductApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _tagProductApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        #endregion Method
    }
}