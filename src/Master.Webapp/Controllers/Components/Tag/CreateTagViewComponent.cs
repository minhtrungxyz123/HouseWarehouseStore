using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Tag
{
    public class CreateTagViewComponent : ViewComponent
    {
        public CreateTagViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new TagModel();
            return View(model);
        }
    }
}