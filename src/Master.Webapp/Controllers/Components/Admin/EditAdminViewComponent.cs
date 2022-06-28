using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Admin
{
    public class EditAdminViewComponent : ViewComponent
    {
        private readonly IAdminApiClient _adminApiClient;
        public EditAdminViewComponent(IAdminApiClient adminApiClient)
        {
            _adminApiClient = adminApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(AdminModel view)
        {
            var model = new AdminModel();
            model.Id = view.Id;
            model.Username = view.Username;
            model.Password = view.Password;
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
            model.FilesModels = await _adminApiClient.GetFilesAdmin(SystemConstants.AdminSettings.NumberOfAdmin);
            return View(model);
        }
    }
}