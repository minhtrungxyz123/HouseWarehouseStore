using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    [Authorize]
    public class UserAccountViewComponent : ViewComponent
    {
        public UserAccountViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new MemberModel();
            var claims = HttpContext.User.Claims;
            if (claims.FirstOrDefault(c => c.Type == "Email") != null)
            {
                var userName = claims.FirstOrDefault(c => c.Type == "Email").Value;
                model.Email = userName;
            }
            else
            {
                model.Email = "Tài Khoản Người Dùng";
            }
            return View(model);
        }
    }
}