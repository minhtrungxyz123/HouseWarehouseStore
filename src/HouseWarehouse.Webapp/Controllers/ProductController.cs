using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers
{
    [Authorize(Roles = "Admin, Member, User")]
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICommentApiClient _commentApiClient;
        private readonly IProductSizeColorApiClient _productSizeColorApiClient;

        public ProductController(IProductApiClient productApiClient,
            ICommentApiClient commentApiClient,
            IProductSizeColorApiClient productSizeColorApiClient)
        {
            _productApiClient = productApiClient;
            _commentApiClient = commentApiClient;
            _productSizeColorApiClient = productSizeColorApiClient;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 9)
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

        public async Task<IActionResult> Detail(string id)
        {
            var result = await _productApiClient.GetProductDetail(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var updateRequest = new ProductModel()
                {
                    Active = model.Active,
                    ProductId = id,
                    CollectionId = model.CollectionId,
                    BarCode = model.BarCode,
                    Body = model.Body,
                    Content = model.Content,
                    CreateBy = model.CreateBy,
                    CreateDate = model.CreateDate,
                    Description = model.Description,
                    DescriptionMeta = model.Description,
                    Factory = model.Factory,
                    GiftInfo = model.GiftInfo,
                    Home = model.Home,
                    Hot = model.Hot,
                    Image = model.Image,
                    Name = model.Name,
                    Price = model.Price,
                    ProductCategorieId = model.ProductCategorieId,
                    Quantity = model.Quantity,
                    QuyCach = model.QuyCach,
                    SaleOff = model.SaleOff,
                    Sort = model.Sort,
                    StatusProduct = model.StatusProduct,
                    TitleMeta = model.TitleMeta,
                    CommentModels = await _commentApiClient.GetById(id),
                    FilesModels = await _productApiClient.GetFilesProduct(SystemConstants.ProductDetailSettings.NumberOfProductDetail, id),
                    ProductSizeColorModels = await _productSizeColorApiClient.GetAll(id)
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
    }
}