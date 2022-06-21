using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Master.Webapp.Controllers.Components.ProductSizeColor
{
    public class CreateProductSizeColorViewComponent : ViewComponent
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IColorApiClient _colorApiClient;
        private readonly ISizeApiClient _sizeApiClient;
        public CreateProductSizeColorViewComponent(IProductApiClient productApiClient,
            IColorApiClient colorApiClient,
            ISizeApiClient sizeApiClient)
        {
            _productApiClient = productApiClient;
            _colorApiClient = colorApiClient;
            _sizeApiClient = sizeApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new ProductSizeColorModel();
            await GetDropDownList(model);
            return View(model);
        }

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