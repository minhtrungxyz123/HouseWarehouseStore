using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.ProductSizeColor
{
    public class DetailProductSizeColorViewComponent : ViewComponent
    {
        public DetailProductSizeColorViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(ProductSizeColorModel view)
        {
            var model = new ProductSizeColorModel();
            model.AvailableProduct = view.AvailableProduct;
            model.ProductsProductId = view.ProductsProductId;
            model.AvailableColor = view.AvailableColor;
            model.AvailableSize = view.AvailableSize;
            model.Id = view.Id;
            return View(model);
        }
    }
}