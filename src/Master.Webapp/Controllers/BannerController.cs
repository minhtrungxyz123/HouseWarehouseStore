using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BannerController : Controller
    {
        #region Fields

        private readonly IBannerApiClient _bannerApiClient;

        public BannerController(IBannerApiClient bannerApiClient)
        {
            _bannerApiClient = bannerApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new BannerSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await
                _bannerApiClient.Get(request);

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
            var model = new BannerModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BannerModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.BannerId = Guid.NewGuid().ToString();
            request.CoverImage = request.BannerId;

            var result = await _bannerApiClient.Create(request);

            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.BannerId = request.BannerId;
                filemodels.filesadd = request.filesadd;
                await _bannerApiClient.CreateImage(filemodels, request.BannerId);

                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _bannerApiClient.GetById(id);

            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new BannerModel()
                {
                    Active = model.Active,
                    BannerId = id,
                    BannerName = model.BannerName,
                    CoverImage = model.CoverImage,
                    Content = model.Content,
                    GroupId = model.GroupId,
                    Height = model.Height,
                    Soft = model.Soft,
                    Title = model.Title,
                    Url = model.Url,
                    Width = model.Width,
                    FilesModels = await _bannerApiClient.GetFilesBanner(SystemConstants.BannerSettings.NumberOfBanner),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BannerModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _bannerApiClient.Edit(request.BannerId, request);
            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.BannerId = request.BannerId;
                filemodels.filesadd = request.filesadd;
                //delete files
                await _bannerApiClient.DeleteFiles(request.BannerId);
                //update files
                await _bannerApiClient.UpdateImage(filemodels, request.BannerId);

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
            var result = await _bannerApiClient.Delete(id);

            await _bannerApiClient.DeleteFiles(id);
            await _bannerApiClient.DeleteDataFiles(id);
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
            var result = await _bannerApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new BannerModel()
                {
                    Active = model.Active,
                    BannerId = id,
                    BannerName = model.BannerName,
                    Width = model.Width,
                    Url = model.Url,
                    Title = model.Title,
                    Content = model.Content,
                    CoverImage = model.CoverImage,
                    GroupId = model.GroupId,
                    Height = model.Height,
                    Soft = model.Soft,
                    FilesModels = await _bannerApiClient.GetFilesBanner(SystemConstants.BannerSettings.NumberOfBanner),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}