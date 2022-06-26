using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProfileController : Controller
    {
        #region Fields

        private readonly IAdminApiClient _adminApiClient;

        public ProfileController(IAdminApiClient adminApiClient)
        {
            _adminApiClient = adminApiClient;
        }

        #endregion Fields
        public async Task<IActionResult> Index(string id)
        {
            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;
            id = userId;

            var result = await _adminApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var model = result.ResultObj;
                var detail = new AdminModel()
                {
                    Active = model.Active,
                    Username = model.Username,
                    Id = id,
                    Role = model.Role,
                    Address = model.Role,
                    Age = model.Age,
                    CreateDate = model.CreateDate,
                    FullName = model.FullName,
                    Image = model.Image,
                    Position = model.Position,
                    Sex = model.Sex,
                    Email = model.Email
                };
                return View(detail);
            }
            return RedirectToAction("Error", "Home");
        }
    }
}
