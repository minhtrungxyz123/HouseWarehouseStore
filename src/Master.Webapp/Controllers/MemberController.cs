using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    public class MemberController : Controller
    {
        #region Fields

        private readonly IMemberApiClient _memberApiClient;

        public MemberController(IMemberApiClient memberApiClient)
        {
            _memberApiClient = memberApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new MemberSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _memberApiClient.Get(request);
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
            var model = new MemberModel();
            model.CreateDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MemberModel request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var hashedPassword = new PasswordHasher<MemberModel>().HashPassword(new MemberModel(), request.Password);
            request.Password = hashedPassword;
            var result = await _memberApiClient.Create(request);

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
            var result = await _memberApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new MemberModel()
                {
                    Email = model.Email,
                    Id = id,
                    Active = model.Active,
                    Address = model.Address,
                    ConfirmEmail = model.ConfirmEmail,
                    CreateDate = model.CreateDate,
                    Fullname = model.Fullname,
                    HomePage = model.HomePage,
                    LockAccount = model.LockAccount,
                    Mobile = model.Mobile,
                    Password = model.Password,
                    Role = model.Role,
                    Token = model.Token
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MemberModel request)
        {
            if (!ModelState.IsValid)
                return View();
            var hashedPassword = new PasswordHasher<AdminModel>().HashPassword(new AdminModel(), request.Password);
            request.Password = hashedPassword;
            var result = await _memberApiClient.Edit(request.Id, request);
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
            var result = await _memberApiClient.Delete(id);
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
            var result = await _memberApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new MemberModel()
                {
                    Email = model.Email,
                    Id = id,
                    Active = model.Active,
                    Address = model.Address,
                    ConfirmEmail = model.ConfirmEmail,
                    CreateDate = model.CreateDate,
                    Fullname = model.Fullname,
                    HomePage = model.HomePage,
                    LockAccount = model.LockAccount,
                    Mobile = model.Mobile,
                    Password = model.Password,
                    Role = model.Role,
                    Token = model.Token
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}