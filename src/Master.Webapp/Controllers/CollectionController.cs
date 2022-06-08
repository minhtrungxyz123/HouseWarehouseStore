using HouseWare.Base;
using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    public class CollectionController : Controller
    {
        #region Fields

        private readonly ICollectionApiClient _collectionApiClient;
        private readonly ISendDiscordHelper _sendDiscordHelper;

        public CollectionController(ICollectionApiClient collectionApiClient,
            ISendDiscordHelper sendDiscordHelper)
        {
            _collectionApiClient = collectionApiClient;
            _sendDiscordHelper = sendDiscordHelper;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new CollectionSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _collectionApiClient.Get(request);
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
            var model = new CollectionModel();
            model.CreateDate = DateTime.UtcNow.ToLocalTime();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CollectionModel request)
        {
            if (!ModelState.IsValid)
                return View(request);
            request.Image = "1";
            request.CreateDate = DateTime.UtcNow.ToLocalTime();
            request.CollectionId = Guid.NewGuid().ToString();

            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "CollectionID").Value;
            request.CreateBy = userId;

            var result = await _collectionApiClient.Create(request);

            if (result)
            {
                await _sendDiscordHelper.SendMessage(_collectionApiClient.GetByUserId(request.CreateBy).Result.Username + " đã thêm mới CR HTC có mã là: " + request.Name);
                var filemodels = new FilesModel();
                filemodels.CollectionId = request.CollectionId;
                filemodels.filesadd = request.filesadd;
                await _collectionApiClient.CreateImage(filemodels, request.CollectionId);

                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _collectionApiClient.GetById(id);

            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new CollectionModel()
                {
                    Active = model.Active,
                    CollectionId = id,
                    Name = model.Name,
                    TitleMeta = model.TitleMeta,
                    StatusProduct = model.StatusProduct,
                    Sort = model.Sort,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    BarCode = model.BarCode,
                    Body = model.Body,
                    Content = model.Content,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    Factory = model.Factory,
                    Home = model.Home,
                    Hot = model.Hot,
                    FilesModels = await _collectionApiClient.GetFilesCollection(SystemConstants.CollectionSettings.NumberOfCollection),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CollectionModel request)
        {
            if (!ModelState.IsValid)
                return View();
            request.Image = "1";
            var result = await _collectionApiClient.Edit(request.CollectionId, request);
            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.CollectionId = request.CollectionId;
                filemodels.filesadd = request.filesadd;
                //delete files
                await _collectionApiClient.DeleteFiles(request.CollectionId);
                //update files
                await _collectionApiClient.UpdateImage(filemodels, request.CollectionId);

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
            var result = await _collectionApiClient.Delete(id);

            await _collectionApiClient.DeleteFiles(id);
            await _collectionApiClient.DeleteDataFiles(id);
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
            var result = await _collectionApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new CollectionModel()
                {
                    Active = model.Active,
                    CollectionId = id,
                    Name = model.Name,
                    BarCode = model.BarCode,
                    Body = model.Body,
                    Content = model.Content,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    Factory = model.Factory,
                    Home = model.Home,
                    Hot = model.Hot,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Sort = model.Sort,
                    StatusProduct = model.StatusProduct,
                    TitleMeta = model.TitleMeta,
                    FilesModels = await _collectionApiClient.GetFilesCollection(SystemConstants.CollectionSettings.NumberOfCollection),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}