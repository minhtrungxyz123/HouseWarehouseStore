using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IMemberApiClient _memberApiClient;

        public RegisterController(IMemberApiClient memberApiClient)
        {
            _memberApiClient = memberApiClient;
        }

        public async Task<IActionResult> SuccessMsg()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return ViewComponent("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MemberModel request)
        {
            request.CreateDate = DateTime.Now;
            request.HomePage = "1";
            request.Active = true;
            request.Role = "Member";
            request.ConfirmEmail = true;
            request.Token = "1";
            request.LockAccount = false;
            var hashedPassword = new PasswordHasher<MemberModel>().HashPassword(new MemberModel(), request.Password);
            request.Password = hashedPassword;
            var result = await _memberApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Đăng ký tài khoản thành công";
                return RedirectToAction("SuccessMsg");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }
    }
}