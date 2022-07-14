using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Size
{
    public class DetailSizeViewComponent : ViewComponent
    {
        public DetailSizeViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(SizeModel view)
        {
            var model = new SizeModel();
            model.SizeId = view.SizeId;
            model.SizeProduct = view.SizeProduct;
            return View(model);
        }
    }
}