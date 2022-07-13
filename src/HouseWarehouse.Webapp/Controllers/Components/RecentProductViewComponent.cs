using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    public class RecentProductViewComponent : ViewComponent
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICommentApiClient _commentApiClient;

        public RecentProductViewComponent(IProductApiClient productApiClient,
            ICommentApiClient commentApiClient)
        {
            _productApiClient = productApiClient;
            _commentApiClient = commentApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = await _productApiClient.GetAll();

            var categories = new List<ProductModel>();
            var data = banner;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new ProductModel
                    {
                        CollectionId = m.CollectionId,
                        Name = m.Name,
                        Active = m.Active,
                        BarCode = m.BarCode,
                        Body = m.Body,
                        Content = m.Content,
                        CreateBy = m.CreateBy,
                        CreateDate = m.CreateDate,
                        Description = m.Description,
                        DescriptionMeta = m.DescriptionMeta,
                        Factory = m.Factory,
                        GiftInfo = m.GiftInfo,
                        Home = m.Home,
                        Hot = m.Hot,
                        Image = m.Image,
                        Price = m.Price,
                        ProductCategorieId = m.ProductCategorieId,
                        ProductId = m.ProductId,
                        Quantity = m.Quantity,
                        QuyCach = m.QuyCach,
                        SaleOff = m.SaleOff,
                        Sort = m.Sort,
                        StatusProduct = m.StatusProduct,
                        TitleMeta = m.TitleMeta,
                        CommentModels = await _commentApiClient.GetById(m.ProductId),
                    };
                    categories.Add(item);
                }
            }

            return View(categories);
        }
    }
}