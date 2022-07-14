using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Size
{
    public class EditSizeViewComponent : ViewComponent
    {
        public EditSizeViewComponent()
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