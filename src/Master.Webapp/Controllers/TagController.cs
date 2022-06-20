using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        #region Fields

        private readonly ITagApiClient _tagApiClient;

        public TagController(ITagApiClient tagApiClient)
        {
            _tagApiClient = tagApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new TagSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _tagApiClient.Get(request);
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
            return ViewComponent("CreateTag");
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _tagApiClient.Create(request);

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
            var result = await _tagApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new TagModel()
                {
                    Active = model.Active,
                    TagId = id,
                    Soft = model.Soft,
                    Name = model.Name
                };
                return ViewComponent("EditTag", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _tagApiClient.Edit(request.TagId, request);
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
            var result = await _tagApiClient.Delete(id);
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
            var result = await _tagApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new TagModel()
                {
                    Active = model.Active,
                    Name = model.Name,
                    TagId = id,
                    Soft = model.Soft
                };
                return ViewComponent("DetailTag", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}