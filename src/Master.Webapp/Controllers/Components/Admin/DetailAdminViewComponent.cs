using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Admin
{
    public class DetailAdminViewComponent : ViewComponent
    {
        public DetailAdminViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(AdminModel view)
        {
            var model = new AdminModel();
            model.Id = view.Id;
            model.Username = view.Username;
            model.Active = view.Active;
            model.Role = view.Role;
            return View(model);
        }
    }
}