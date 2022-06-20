using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Tag
{
    public class DetailTagViewComponent : ViewComponent
    {
        public DetailTagViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(TagModel view)
        {
            var model = new TagModel();
            model.TagId = view.TagId;
            model.Name = view.Name;
            model.Active = view.Active;
            model.Soft = view.Soft;
            return View(model);
        }
    }
}