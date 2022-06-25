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
            model.Email = view.Email;
            model.CreateDate = view.CreateDate;
            model.Age = view.Age;
            model.Address = view.Address;
            model.FullName = view.FullName;
            model.Image = view.Image;
            model.Position = view.Position;
            model.Sex = view.Sex;
            return View(model);
        }
    }
}