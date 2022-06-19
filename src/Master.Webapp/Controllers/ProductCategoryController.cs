using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Create()
        {
            var model = new ProductCategoryModel();
            await GetDropDownList(model);
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
                await GetDropDownList(model);
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
                    AvailablePath = model.AvailablePath,
                    Url = model.Url
                };

                if (model.AvailablePath.Count > 0 &&
                !string.IsNullOrEmpty(model.ParentId))
                {
                    var item = model.AvailablePath
                        .FirstOrDefault(x => x.Value.Equals(model.ParentId));

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }

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
                await GetDropDownList(model);
                var updateRequest = new ProductCategoryModel()
                {
                    Active = model.Active,
                    ProductCategorieId = id,
                    Name = model.Name,
                    Body = model.Body,
                    Home = model.Home,
                    TitleMeta = model.TitleMeta,
                    Url = model.Url,
                    AvailablePath = model.AvailablePath,
                    Image = model.Image,
                    CoverImage = model.CoverImage,
                    DescriptionMeta = model.DescriptionMeta,
                    Soft = model.Soft
                };

                if (model.AvailablePath.Count > 0 &&
                !string.IsNullOrEmpty(model.ParentId))
                {
                    var item = model.AvailablePath
                        .FirstOrDefault(x => x.Value.Equals(model.ParentId));

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }

                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method

        #region Utilities

        private async Task GetDropDownList(ProductCategoryModel model)
        {
            var availableCategory = await _productCategoryApiCient.GetPath();

            var categories = new List<SelectListItem>();
            var data = availableCategory;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.ProductCategorieId,
                    };
                    categories.Add(item);
                }
            }
            categories.OrderBy(e => e.Text);
            if (categories == null || categories.Count == 0)
            {
                categories = new List<SelectListItem>();
            }

            model.AvailablePath = new List<SelectListItem>(categories);
        }

        #endregion Utilities
    }
}