using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Admin
{
    public class EditAdminViewComponent : ViewComponent
    {
        public EditAdminViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(AdminModel view)
        {
            var model = new AdminModel();
            model.Id = view.Id;
            model.Username = view.Username;
            model.Password = view.Password;
            model.Active = view.Active;
            model.Role = view.Role;
            return View(model);
        }
    }
}