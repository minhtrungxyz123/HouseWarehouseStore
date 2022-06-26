using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }

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

            var hashedPassword = new PasswordHasher<AdminModel>().HashPassword(new AdminModel(), request.Password);
            request.Password = hashedPassword;
            request.Image = "1";
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
                    Id = id,
                    Role = model.Role,
                    Username = model.Username,
                    Position = model.Position,
                    Image = model.Image,
                    Sex = model.Sex,
                    FullName = model.FullName,
                    Age = model.Age,
                    Address = model.Address,
                    CreateDate = model.CreateDate,
                    Email = model.Email
                };
                return ViewComponent("EditAdmin", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminModel request)
        {
            request.Image = "1";
            if (!ModelState.IsValid)
                return View();

            var hashedPassword = new PasswordHasher<AdminModel>().HashPassword(new AdminModel(), request.Password);
            request.Password = hashedPassword;

            var result = await _adminApiClient.Edit(request.Id, request);
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
                    Id = id,
                    Role = model.Role,
                    Address = model.Role,
                    Age = model.Age,
                    CreateDate = model.CreateDate,
                    FullName = model.FullName,
                    Image = model.Image,
                    Position = model.Position,
                    Sex = model.Sex,
                    Email = model.Email
                };
                return ViewComponent("DetailAdmin", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion

        #region Utilities

        #endregion
    }
}
