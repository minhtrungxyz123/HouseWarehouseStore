using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Master.Webapp.Controllers.Components.Size
{
    public class CreateSizeViewComponent : ViewComponent
    {
        private readonly IProductApiClient _productApiCient;
        public CreateSizeViewComponent(IProductApiClient productApiCient)
        {
            _productApiCient = productApiCient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SizeModel();
            await GetDropDownList(model);
            return View(model);
        }

        #region Utilities

        private async Task GetDropDownList(SizeModel model)
        {
            var availableProd = await _productApiCient.GetActive();

            var categories = new List<SelectListItem>();
            var data = availableProd;

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
        }

        #endregion Utilities
    }
}