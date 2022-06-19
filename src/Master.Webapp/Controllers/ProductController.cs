using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        #region Fields

        private readonly IProductApiClient _productApiCient;
        private readonly ICollectionApiClient _collectionApiClient;
        private readonly IProductCategoryApiCient _productCategoryApiCient;

        public ProductController(IProductApiClient productApiCient,
            ICollectionApiClient collectionApiClient,
            IProductCategoryApiCient productCategoryApiCient)
        {
            _productApiCient = productApiCient;
            _collectionApiClient = collectionApiClient;
            _productCategoryApiCient = productCategoryApiCient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ProductSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _productApiCient.Get(request);

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
            var model = new ProductModel();
            await GetDropDownList(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.ProductId = Guid.NewGuid().ToString();
            request.Image = request.ProductId;
            request.CreateDate = DateTime.UtcNow.ToLocalTime();
            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;
            request.CreateBy = userId;

            var result = await _productApiCient.Create(request);

            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.ProductId = request.ProductId;
                filemodels.filesadd = request.filesadd;
                //
                await _productApiCient.CreateImage(filemodels, request.ProductId);

                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _productApiCient.GetById(id);

            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                await GetDropDownList(model);
                var updateRequest = new ProductModel()
                {
                    Active = model.Active,
                    ProductId = id,
                    Name = model.Name,
                    TitleMeta = model.TitleMeta,
                    Body = model.Body,
                    Home = model.Home,
                    BarCode = model.BarCode,
                    AvailableCollection = model.AvailableCollection,
                    Content = model.Content,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    DescriptionMeta = model.DescriptionMeta,
                    Factory = model.Factory,
                    GiftInfo = model.GiftInfo,
                    Hot = model.Hot,
                    Image = model.Image,
                    Price = model.Price,
                    AvailableProductCategory = model.AvailableProductCategory,
                    Quantity = model.Quantity,
                    QuyCach = model.QuyCach,
                    SaleOff = model.SaleOff,
                    Sort = model.Sort,
                    StatusProduct = model.StatusProduct,
                    FilesModels = await _productApiCient.GetFilesProduct(SystemConstants.ProductSettings.NumberOfProduct)
                };

                if (model.AvailableCollection.Count > 0 &&
                !string.IsNullOrEmpty(model.CollectionId))
                {
                    var item = model.AvailableCollection
                        .FirstOrDefault(x => x.Value.Equals(model.CollectionId));

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }

                if (model.AvailableProductCategory.Count > 0 &&
                !string.IsNullOrEmpty(model.ProductCategorieId))
                {
                    var item1 = model.AvailableProductCategory
                        .FirstOrDefault(x => x.Value.Equals(model.ProductCategorieId));

                    if (item1 != null)
                    {
                        item1.Selected = true;
                    }
                }

                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiCient.Edit(request.ProductId, request);
            if (result)
            {
                var filemodels = new FilesModel();
                filemodels.ProductId = request.ProductId;
                filemodels.filesadd = request.filesadd;
                //delete files
                await _productApiCient.DeleteFiles(request.ProductId);
                //update files
                await _productApiCient.UpdateImage(filemodels, request.ProductId);

                TempData["result"] = "Sửa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var result = await _productApiCient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                await GetDropDownList(model);
                var updateRequest = new ProductModel()
                {
                    Active = model.Active,
                    ProductId = id,
                    Name = model.Name,
                    Body = model.Body,
                    Home = model.Home,
                    TitleMeta = model.TitleMeta,
                    Image = model.Image,
                    DescriptionMeta = model.DescriptionMeta,
                    StatusProduct = model.StatusProduct,
                    Sort = model.Sort,
                    SaleOff = model.SaleOff,
                    QuyCach = model.QuyCach,
                    Quantity = model.Quantity,
                    AvailableProductCategory = model.AvailableProductCategory,
                    Price = model.Price,
                    BarCode = model.BarCode,
                    AvailableCollection = model.AvailableCollection,
                    Content = model.Content,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    Factory = model.Factory,
                    GiftInfo = model.GiftInfo,
                    Hot = model.Hot,
                    FilesModels = await _productApiCient.GetFilesProduct(SystemConstants.ProductSettings.NumberOfProduct)
                };

                if (model.AvailableCollection.Count > 0 &&
                !string.IsNullOrEmpty(model.CollectionId))
                {
                    var item = model.AvailableCollection
                        .FirstOrDefault(x => x.Value.Equals(model.CollectionId));

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }

                if (model.AvailableProductCategory.Count > 0 &&
                !string.IsNullOrEmpty(model.ProductCategorieId))
                {
                    var item1 = model.AvailableProductCategory
                        .FirstOrDefault(x => x.Value.Equals(model.ProductCategorieId));

                    if (item1 != null)
                    {
                        item1.Selected = true;
                    }
                }

                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method

        #region Utilities

        private async Task GetDropDownList(ProductModel model)
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

            model.AvailableProductCategory = new List<SelectListItem>(categories);

            //Collection
            var availableCollection = await _collectionApiClient.GetActive();

            var collection = new List<SelectListItem>();
            var data1 = availableCollection;

            if (data1?.Count > 0)
            {
                foreach (var m1 in data1)
                {
                    var item1 = new SelectListItem
                    {
                        Text = m1.Name,
                        Value = m1.CollectionId,
                    };
                    collection.Add(item1);
                }
            }
            collection.OrderBy(e => e.Text);
            if (collection == null || collection.Count == 0)
            {
                collection = new List<SelectListItem>();
            }

            model.AvailableCollection = new List<SelectListItem>(collection);
        }

        #endregion Utilities
    }
}