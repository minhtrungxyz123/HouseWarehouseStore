using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    public class CollectionController : Controller
    {
        #region Fields

        private readonly ICollectionApiClient _collectionApiClient;

        public CollectionController(ICollectionApiClient collectionApiClient)
        {
            _collectionApiClient = collectionApiClient;
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
            var result = await _collectionApiClient.Create(request);

            if (result)
            {
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
                    Image = model.Image,
                    BarCode = model.BarCode,
                    Body = model.Body,
                    Content = model.Content,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    Factory = model.Factory,
                    Home = model.Home,
                    Hot = model.Hot,
                };
                return ViewComponent("EditCollection", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CollectionModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _collectionApiClient.Edit(request.CollectionId, request);
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
            var result = await _collectionApiClient.Delete(id);
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
                    Image = model.Image,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Sort = model.Sort,
                    StatusProduct = model.StatusProduct,
                    TitleMeta = model.TitleMeta,
                };
                return ViewComponent("DetailCollection", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}