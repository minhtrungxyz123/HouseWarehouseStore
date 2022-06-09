using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Admin
{
    public class CreateAdminViewComponent : ViewComponent
    {
        public CreateAdminViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new AdminModel();
            return View(model);
        }
    }
}