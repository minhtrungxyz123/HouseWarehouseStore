using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        #region Fields

        private readonly IContactApiClient _contactApiClient;

        public ContactController(IContactApiClient contactApiClient)
        {
            _contactApiClient = contactApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ContactSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _contactApiClient.Get(request);
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
            var model = new ContactModel();
            model.CreateDate = DateTime.UtcNow.ToLocalTime();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _contactApiClient.Create(request);

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
            var result = await _contactApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ContactModel()
                {
                    Address = model.Address,
                    ContactId = id,
                    Fullname = model.Fullname,
                    Body = model.Body,
                    CreateDate = model.CreateDate,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    Subject = model.Subject,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _contactApiClient.Edit(request.ContactId, request);
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
            var result = await _contactApiClient.Delete(id);
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
            var result = await _contactApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ContactModel()
                {
                    Fullname = model.Fullname,
                    Address = model.Address,
                    ContactId = id,
                    Subject = model.Subject,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    CreateDate = model.CreateDate,
                    Body = model.Body,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion
    }
}
