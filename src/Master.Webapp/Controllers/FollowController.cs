using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    public class FollowController : Controller
    {
        #region Fields

        private readonly IFollowApiClient _followApiClient;

        public FollowController(IFollowApiClient followApiClient)
        {
            _followApiClient = followApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new FollowSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _followApiClient.Get(request);
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
            return ViewComponent("CreateFollow");
        }

        [HttpPost]
        public async Task<IActionResult> Create(FollowModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _followApiClient.Create(request);

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
            var result = await _followApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new FollowModel()
                {
                    Facebook = model.Facebook,
                    FollowId = id,
                    Icon = model.Icon,
                    Instagram = model.Instagram,
                    Linkedin = model.Linkedin,
                    Twitter = model.Twitter,
                    Youtube = model.Youtube
                };
                return ViewComponent("EditFollow", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FollowModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _followApiClient.Edit(request.FollowId, request);
            if (result)
            {
                TempData["result"] = "Sửa thành công";
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
            var result = await _followApiClient.Delete(id);
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
            var result = await _followApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new FollowModel()
                {
                    Facebook = model.Facebook,
                    FollowId = id,
                    Icon = model.Icon,
                    Instagram = model.Instagram,
                    Linkedin = model.Linkedin,
                    Twitter = model.Twitter,
                    Youtube = model.Youtube
                };
                return ViewComponent("DetailFollow", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}