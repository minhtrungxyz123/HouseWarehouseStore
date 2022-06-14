using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigSiteController : Controller
    {
        #region Fields

        private readonly IConfigSiteApiClient _configSiteApiClient;

        public ConfigSiteController(IConfigSiteApiClient configSiteApiClient)
        {
            _configSiteApiClient = configSiteApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ConfigSiteSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _configSiteApiClient.Get(request);

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
            var model = new ConfigSiteModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ConfigSiteModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.ConfigSiteId = Guid.NewGuid().ToString();
            request.CoverImage = request.ConfigSiteId;

            var result = await _configSiteApiClient.Create(request);

            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.ConfigsiteId = request.ConfigSiteId;
                filemodels.filesadd = request.filesadd;
                await _configSiteApiClient.CreateImage(filemodels, request.ConfigSiteId);

                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _configSiteApiClient.GetById(id);

            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ConfigSiteModel()
                {
                    GoogleMap = model.GoogleMap,
                    ConfigSiteId = id,
                    CoverImage = model.CoverImage,
                    ContactInfo = model.ContactInfo,
                    Description = model.Description,
                    Email = model.Email,
                    Facebook = model.Facebook,
                    FbMessage = model.FbMessage,
                    FooterInfo = model.FooterInfo,
                    GoogleAnalytics = model.GoogleAnalytics,
                    GooglePlus = model.GooglePlus,
                    Hotline = model.Hotline,
                    Linkedin = model.Linkedin,
                    LiveChat = model.LiveChat,
                    nameShopee = model.nameShopee,
                    SaleOffProgram = model.SaleOffProgram,
                    Title = model.Title,
                    Twitter = model.Twitter,
                    urlWeb = model.urlWeb,
                    Youtube = model.Youtube,
                    FilesModels = await _configSiteApiClient.GetFilesConfigSite(SystemConstants.ConfigSiteSettings.NumberOfConfigSite),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ConfigSiteModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _configSiteApiClient.Edit(request.ConfigSiteId, request);
            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.ConfigsiteId = request.ConfigSiteId;
                filemodels.filesadd = request.filesadd;
                //delete files
                await _configSiteApiClient.DeleteFiles(request.ConfigSiteId);
                //update files
                await _configSiteApiClient.UpdateImage(filemodels, request.ConfigSiteId);

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
            var result = await _configSiteApiClient.Delete(id);

            await _configSiteApiClient.DeleteFiles(id);
            await _configSiteApiClient.DeleteDataFiles(id);
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
            var result = await _configSiteApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ConfigSiteModel()
                {
                    nameShopee = model.nameShopee,
                    ConfigSiteId = id,
                    Description = model.Description,
                    Youtube = model.Youtube,
                    ContactInfo = model.ContactInfo,
                    CoverImage = model.CoverImage,
                    Email = model.Email,
                    Facebook = model.Facebook,
                    FbMessage = model.FbMessage,
                    FooterInfo = model.FooterInfo,
                    GoogleAnalytics = model.GoogleAnalytics,
                    GoogleMap = model.GoogleMap,
                    GooglePlus = model.GooglePlus,
                    Hotline = model.Hotline,
                    Linkedin = model.Linkedin,
                    LiveChat = model.LiveChat,
                    SaleOffProgram = model.SaleOffProgram,
                    Title = model.Title,
                    Twitter = model.Twitter,
                    urlWeb = model.urlWeb,
                    FilesModels = await _configSiteApiClient.GetFilesConfigSite(SystemConstants.ConfigSiteSettings.NumberOfConfigSite),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}