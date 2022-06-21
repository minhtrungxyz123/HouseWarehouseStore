using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductSizeColorController : Controller
    {
        #region Fields

        private readonly IProductSizeColorApiClient _productSizeColorApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly IColorApiClient _colorApiClient;
        private readonly ISizeApiClient _sizeApiClient;

        public ProductSizeColorController(IProductSizeColorApiClient productSizeColorApiClient,
            IProductApiClient productApiClient,
            IColorApiClient colorApiClient,
            ISizeApiClient sizeApiClient)
        {
            _productSizeColorApiClient = productSizeColorApiClient;
            _productApiClient = productApiClient;
            _colorApiClient = colorApiClient;
            _sizeApiClient = sizeApiClient;
        }

        #endregion Fields

        #region List

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ProductSizeColorSearchModel()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _productSizeColorApiClient.Get(request);
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
            return ViewComponent("CreateProductSizeColor");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductSizeColorModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productSizeColorApiClient.Create(request);

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
            var result = await _productSizeColorApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                await GetDropDownList(model);
                var updateRequest = new ProductSizeColorModel()
                {
                    AvailableProduct = model.AvailableProduct,
                    Id = id,
                    AvailableColor = model.AvailableColor,
                    ProductsProductId = model.ProductsProductId,
                    AvailableSize = model.AvailableSize
                };
                if (model.AvailableProduct.Count > 0 &&
                !string.IsNullOrEmpty(model.ProductId))
                {
                    var item = model.AvailableProduct
                        .FirstOrDefault(x => x.Value.Equals(model.ProductId));

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }

                if (model.AvailableColor.Count > 0 &&
                !string.IsNullOrEmpty(model.ColorId))
                {
                    var item1 = model.AvailableColor
                        .FirstOrDefault(x => x.Value.Equals(model.ColorId));

                    if (item1 != null)
                    {
                        item1.Selected = true;
                    }
                }
                if (model.AvailableSize.Count > 0 &&
                !string.IsNullOrEmpty(model.SizeId))
                {
                    var item1 = model.AvailableSize
                        .FirstOrDefault(x => x.Value.Equals(model.SizeId));

                    if (item1 != null)
                    {
                        item1.Selected = true;
                    }
                }
                return ViewComponent("EditProductSizeColor", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductSizeColorModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productSizeColorApiClient.Edit(request.Id, request);
            if (result)
            {
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
            var result = await _productSizeColorApiClient.Delete(id);
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
            var result = await _productSizeColorApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                await GetDropDownList(model);
                var updateRequest = new ProductSizeColorModel()
                {
                    AvailableSize = model.AvailableSize,
                    Id = id,
                    ProductsProductId = model.ProductsProductId,
                    AvailableColor = model.AvailableColor,
                    AvailableProduct = model.AvailableProduct
                };

                if (model.AvailableProduct.Count > 0 &&
                !string.IsNullOrEmpty(model.ProductId))
                {
                    var item = model.AvailableProduct
                        .FirstOrDefault(x => x.Value.Equals(model.ProductId));

                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }

                if (model.AvailableColor.Count > 0 &&
                !string.IsNullOrEmpty(model.ColorId))
                {
                    var item1 = model.AvailableColor
                        .FirstOrDefault(x => x.Value.Equals(model.ColorId));

                    if (item1 != null)
                    {
                        item1.Selected = true;
                    }
                }
                if (model.AvailableSize.Count > 0 &&
                !string.IsNullOrEmpty(model.SizeId))
                {
                    var item1 = model.AvailableSize
                        .FirstOrDefault(x => x.Value.Equals(model.SizeId));

                    if (item1 != null)
                    {
                        item1.Selected = true;
                    }
                }

                return ViewComponent("DetailProductSizeColor", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion Method

        #region Utilities

        private async Task GetDropDownList(ProductSizeColorModel model)
        {
            var availableProduct = await _productApiClient.GetActive();
            var availableColor = await _colorApiClient.GetActive();
            var availableSize = await _sizeApiClient.GetActive();

            var categories = new List<SelectListItem>();
            var data = availableProduct;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.ProductId,
                    };
                    categories.Add(item);
                }
            }
            categories.OrderBy(e => e.Text);
            if (categories == null || categories.Count == 0)
            {
                categories = new List<SelectListItem>();
            }

            model.AvailableProduct = new List<SelectListItem>(categories);

            //
            var data1 = availableColor;

            if (data1?.Count > 0)
            {
                foreach (var m in data1)
                {
                    var item = new SelectListItem
                    {
                        Text = m.NameColor,
                        Value = m.ColorId,
                    };
                    categories.Add(item);
                }
            }
            categories.OrderBy(e => e.Text);
            if (categories == null || categories.Count == 0)
            {
                categories = new List<SelectListItem>();
            }

            model.AvailableColor = new List<SelectListItem>(categories);

            //
            var data2 = availableSize;

            if (data2?.Count > 0)
            {
                foreach (var m in data2)
                {
                    var item = new SelectListItem
                    {
                        Text = m.SizeProduct,
                        Value = m.SizeId,
                    };
                    categories.Add(item);
                }
            }
            categories.OrderBy(e => e.Text);
            if (categories == null || categories.Count == 0)
            {
                categories = new List<SelectListItem>();
            }

            model.AvailableSize = new List<SelectListItem>(categories);
        }

        #endregion Utilities
    }
}
