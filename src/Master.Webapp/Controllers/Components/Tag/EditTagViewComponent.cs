using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Admin
{
    public class EditTagViewComponent : ViewComponent
    {
        public EditTagViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(TagModel view)
        {
            var model = new TagModel();
            model.TagId = view.TagId;
            model.Name = view.Name;
            model.Soft = view.Soft;
            model.Active = view.Active;
            return View(model);
        }
    }
}