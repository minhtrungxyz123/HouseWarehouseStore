using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        #region Fields

        private readonly IProductCategoryApiCient _productCategoryApiCient;

        public ProductCategoryController(IProductCategoryApiCient productCategoryApiCient)
        {
            _productCategoryApiCient = productCategoryApiCient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ProductCategorySearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _productCategoryApiCient.Get(request);

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
            var model = new ProductCategoryModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.ProductCategorieId = Guid.NewGuid().ToString();
            request.Image = request.ProductCategorieId;
            request.CoverImage = request.ProductCategorieId;

            var result = await _productCategoryApiCient.Create(request);

            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.ProductCategoryId = request.ProductCategorieId;
                filemodels.filesadd = request.filesadd;
                await _productCategoryApiCient.CreateImage(filemodels, request.ProductCategorieId);

                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _productCategoryApiCient.GetById(id);

            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ProductCategoryModel()
                {
                    Active = model.Active,
                    ProductCategorieId = id,
                    Name = model.Name,
                    TitleMeta = model.TitleMeta,
                    Body = model.Body,
                    Home = model.Home,
                    CoverImage = model.CoverImage,
                    Soft = model.Soft,
                    DescriptionMeta = model.DescriptionMeta,
                    Image = model.Image,
                    ParentId = model.ParentId,
                    Url = model.Url,
                    FilesModels = await _productCategoryApiCient.GetFilesProductCategory(SystemConstants.ProductCategorySettings.NumberOfProductCategory),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategoryModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productCategoryApiCient.Edit(request.ProductCategorieId, request);
            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.ProductCategoryId = request.ProductCategorieId;
                filemodels.filesadd = request.filesadd;
                //delete files
                await _productCategoryApiCient.DeleteFiles(request.ProductCategorieId);
                //update files
                await _productCategoryApiCient.UpdateImage(filemodels, request.ProductCategorieId);

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
            var result = await _productCategoryApiCient.Delete(id);

            await _productCategoryApiCient.DeleteFiles(id);
            await _productCategoryApiCient.DeleteDataFiles(id);
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
            var result = await _productCategoryApiCient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ProductCategoryModel()
                {
                    Active = model.Active,
                    ProductCategorieId = id,
                    Name = model.Name,
                    Body = model.Body,
                    Home = model.Home,
                    TitleMeta = model.TitleMeta,
                    Url = model.Url,
                    ParentId = model.ParentId,
                    Image = model.Image,
                    CoverImage = model.CoverImage,
                    DescriptionMeta = model.DescriptionMeta,
                    Soft =model.Soft,
                    FilesModels = await _productCategoryApiCient.GetFilesProductCategory(SystemConstants.ProductCategorySettings.NumberOfProductCategory),
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}