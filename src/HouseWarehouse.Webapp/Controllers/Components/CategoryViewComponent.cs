using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public CategoryViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = await _categoryApiClient.GetAll();

            var categories = new List<ProductCategoryModel>();
            var data = banner;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new ProductCategoryModel
                    {
                        ParentId = m.ParentId,
                        Name = m.Name,
                        Active = m.Active,
                        Body = m.Body,
                        CoverImage = m.CoverImage,
                        DescriptionMeta = m.DescriptionMeta,
                        Home = m.Home,
                        Icon = m.Icon,
                        Image = m.Image,
                        ProductCategorieId = m.ProductCategorieId,
                        Soft = m.Soft,
                        TitleMeta = m.TitleMeta,
                        Url = m.Url
                    };
                    categories.Add(item);
                }
            }

            return View(categories);
        }
    }
}