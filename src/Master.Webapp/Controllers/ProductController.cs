using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    public class ProductController : Controller
    {
        #region Fields

        private readonly IProductApiClient _productApiClient;

        public ProductController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
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
            var data = await _productApiClient.Get(request);
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
            return ViewComponent("CreateProduct");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.Create(request);

            if (result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ProductModel()
                {
                    Active = model.Active,
                    ProductId = id,
                    Name = model.Name,
                    BarCode = model.BarCode,
                    Body = model.Body,
                    CollectionId = model.CollectionId,
                    Content = model.Content,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    DescriptionMeta = model.DescriptionMeta,
                    Factory = model.Factory,
                    GiftInfo = model.GiftInfo,
                    Home = model.Home,
                    Hot = model.Hot,
                    Image = model.Image,
                    Price = model.Price,
                    ProductCategorieId =model.ProductCategorieId,
                    Quantity = model.Quantity,
                    QuyCach = model.QuyCach,
                    SaleOff = model.SaleOff,
                    Sort = model.Sort,
                    StatusProduct = model.StatusProduct,
                    TitleMeta= model.TitleMeta
                };
                return ViewComponent("EditProduct", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.Edit(request.ProductId, request);
            if (result)
            {
                TempData["result"] = "Sửa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _productApiClient.Delete(id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _productApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ProductModel()
                {
                    Active = model.Active,
                    ProductId = id,
                    Name = model.Name,
                    TitleMeta = model.TitleMeta,
                    StatusProduct = model.StatusProduct,
                    Sort = model.Sort,
                    SaleOff = model.SaleOff,
                    QuyCach = model.QuyCach,
                    Quantity = model.Quantity,
                    BarCode = model.BarCode,
                    Body = model.Body,
                    CollectionId = model.CollectionId,
                    Content = model.Content,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    DescriptionMeta = model.DescriptionMeta,
                    Factory = model.Factory,
                    GiftInfo = model.GiftInfo,
                    Home = model.Home,
                    Hot = model.Hot,
                    Image = model.Image,
                    Price = model.Price,
                    ProductCategorieId = model.ProductCategorieId
                };
                return ViewComponent("DetailProduct", updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        #endregion
    }
}
