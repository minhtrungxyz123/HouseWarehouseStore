using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Size
{
    public class CreateSizeViewComponent : ViewComponent
    {
        public CreateSizeViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SizeModel();
            return View(model);
        }
    }
}