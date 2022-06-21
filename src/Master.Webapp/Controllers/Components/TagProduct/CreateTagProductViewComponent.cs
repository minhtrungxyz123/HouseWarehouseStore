using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Master.Webapp.Controllers.Components.TagProduct
{
    public class CreateTagProductViewComponent : ViewComponent
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ITagApiClient _tagApiClient;
        public CreateTagProductViewComponent(IProductApiClient productApiClient,
            ITagApiClient tagApiClient)
        {
            _productApiClient = productApiClient;
            _tagApiClient = tagApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new TagProductModel();
            await GetDropDownList(model);
            return View(model);
        }

        #region Utilities

        private async Task GetDropDownList(TagProductModel model)
        {
            var availableProduct = await _productApiClient.GetActive();
            var availableTag = await _tagApiClient.GetActive();

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
            var data1 = availableTag;

            if (data1?.Count > 0)
            {
                foreach (var m1 in data1)
                {
                    var item1 = new SelectListItem
                    {
                        Text = m1.Name,
                        Value = m1.TagId,
                    };
                    categories.Add(item1);
                }
            }
            categories.OrderBy(e => e.Text);
            if (categories == null || categories.Count == 0)
            {
                categories = new List<SelectListItem>();
            }

            model.AvailableTag = new List<SelectListItem>(categories);
        }

        #endregion Utilities
    }
}