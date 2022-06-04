using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    public class SizeController : Controller
    {
        #region Fields

        private readonly ISizeApiClient _sizeApiClient;

        public SizeController(ISizeApiClient sizeApiClient)
        {
            _sizeApiClient = sizeApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new SizeSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _sizeApiClient.Get(request);
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
        public ActionResult Create()
        {
            return ViewComponent("CreateSize");
        }

        [HttpPost]
        public async Task<IActionResult> Create(SizeModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _sizeApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _sizeApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new SizeModel()
                {
                    SizeProduct = model.SizeProduct,
                    SizeId = id,
                };
                return ViewComponent("EditSize", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SizeModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _sizeApiClient.Edit(request.SizeId, request);
            if (result)
            {
                TempData["result"] = "Sửa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _sizeApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var result = await _sizeApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new SizeModel()
                {
                    SizeProduct = model.SizeProduct,
                    SizeId = id,
                };
                return ViewComponent("DetailSize", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion
    }
}
