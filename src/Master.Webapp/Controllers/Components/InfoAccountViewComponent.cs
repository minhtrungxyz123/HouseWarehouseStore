using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components
{
    [Authorize]
    public class InfoAccountViewComponent : ViewComponent
    {
        private readonly ILogger<InfoAccountViewComponent> _logger;

        public InfoAccountViewComponent(ILogger<InfoAccountViewComponent> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _logger.LogInformation("Get SetCookie ");
            var model = new AdminModel();
            var claims = HttpContext.User.Claims;
            var userName = claims.FirstOrDefault(c => c.Type == "Username").Value;
            model.Username = userName;
            _logger.LogInformation("End get SetCookie");
            return View(model);
        }
    }
}