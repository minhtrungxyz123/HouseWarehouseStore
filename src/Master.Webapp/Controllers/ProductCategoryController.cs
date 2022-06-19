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
            request.CoverImage = request.Name;

            var result = await _productCategoryApiCient.Create(request);

            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.ProductCategoryId = request.ProductCategorieId;
                filemodels.filesadd = request.filesadd;
                filemodels.Coverfilesadd = request.Coverfilesadd;

                //
                await _productCategoryApiCient.CreateImage(filemodels, request.ProductCategorieId);

                //
                await _productCategoryApiCient.CreateImageConver(filemodels, request.Name);

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
                    Url = model.Url
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

                //cover image
                filemodels.Coverfilesadd = request.Coverfilesadd;
                await _productCategoryApiCient.DeleteFilesCover(request.Name);
                await _productCategoryApiCient.UpdateImageCover(filemodels, request.Name);

                TempData["result"] = "Sửa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }


        [HttpGet]
        public IActionResult Delete(string id, string name)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return View(new ProductCategoryModel()
            {
                ProductCategorieId = id,
                Name = name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductCategoryModel request)
        {
            var result = await _productCategoryApiCient.Delete(request.ProductCategorieId);

            await _productCategoryApiCient.DeleteFiles(request.ProductCategorieId);
            await _productCategoryApiCient.DeleteDataFiles(request.ProductCategorieId);

            //cover image
            await _productCategoryApiCient.DeleteFilesCover(request.Name);
            await _productCategoryApiCient.DeleteDataFilesCover(request.Name);

            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
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
                    Soft = model.Soft
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method
    }
}