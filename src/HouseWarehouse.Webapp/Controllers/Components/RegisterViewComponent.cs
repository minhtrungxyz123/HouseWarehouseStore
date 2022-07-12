using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    public class RegisterViewComponent : ViewComponent
    {
        public RegisterViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new MemberModel();
            return View(model);
        }
    }
}