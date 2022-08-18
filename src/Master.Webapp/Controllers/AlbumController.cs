using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AlbumController : Controller
    {
        #region Fields

        private readonly IAlbumApiClient _albumApiClient;

        public AlbumController(IAlbumApiClient albumApiClient)
        {
            _albumApiClient = albumApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new AlbumSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _albumApiClient.Get(request);

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
            var model = new AlbumModel();
            model.CreateDate = DateTime.UtcNow.ToLocalTime();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.CreateDate = DateTime.UtcNow.ToLocalTime();
            request.AlbumId = Guid.NewGuid().ToString();
            request.ListImage = request.AlbumId;

            var result = await _albumApiClient.Create(request);

            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.AlbumId = request.AlbumId;
                filemodels.filesadd = request.filesadd;
                await _albumApiClient.CreateImage(filemodels, request.AlbumId);

                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _albumApiClient.GetById(id);

            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new AlbumModel()
                {
                    Active = model.Active,
                    AlbumId = id,
                    Name = model.Name,
                    Sort = model.Sort,
                    Body = model.Body,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    Home = model.Home,
                    ListImage = model.ListImage,
                    Title = model.Title,
                    FilesModels = await _albumApiClient.GetFilesAlbum(SystemConstants.AlbumSettings.NumberOfAlbum),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AlbumModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _albumApiClient.Edit(request.AlbumId, request);
            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.AlbumId = request.AlbumId;
                filemodels.filesadd = request.filesadd;
                //delete files
                await _albumApiClient.DeleteFiles(request.AlbumId);
                //update files
                await _albumApiClient.UpdateImage(filemodels, request.AlbumId);

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
            var result = await _albumApiClient.Delete(id);

            await _albumApiClient.DeleteFiles(id);
            await _albumApiClient.DeleteDataFiles(id);
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
            var result = await _albumApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new AlbumModel()
                {
                    Active = model.Active,
                    AlbumId = id,
                    Name = model.Name,
                    Body = model.Body,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    Home = model.Home,
                    Sort = model.Sort,
                    Title = model.Title,
                    FilesModels = await _albumApiClient.GetFilesAlbum(SystemConstants.AlbumSettings.NumberOfAlbum),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}