using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    public class AdminController : Controller
    {
        #region Fields

        private readonly IAdminApiClient _adminApiClient;

        public AdminController(IAdminApiClient adminApiClient)
        {
            _adminApiClient = adminApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new AdminSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _adminApiClient.Get(request);
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
            return ViewComponent("CreateAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _adminApiClient.Create(request);

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
            var result = await _adminApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new AdminModel()
                {
                    Active = model.Active,
                    AdminId = id,
                    Role=model.Role,
                    Username = model.Username
                };
                return ViewComponent("EditAdmin", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _adminApiClient.Edit(request.AdminId, request);
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
            var result = await _adminApiClient.Delete(id);
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
            var result = await _adminApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new AdminModel()
                {
                    Active = model.Active,
                    Username = model.Username,
                    AdminId = id,
                    Role=model.Role
                };
                return ViewComponent("DetailAdmin", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion
    }
}
