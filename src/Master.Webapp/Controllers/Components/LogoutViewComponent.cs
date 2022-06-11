using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components
{
    public class LogoutViewComponent : ViewComponent
    {
        private readonly ILogger<LogoutViewComponent> _logger;

        public LogoutViewComponent(ILogger<LogoutViewComponent> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}