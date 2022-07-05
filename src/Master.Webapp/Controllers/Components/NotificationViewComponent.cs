using HouseWarehouseStore.Models;
using Master.Webapp.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components
{
    public class NotificationViewComponent : ViewComponent
    {
        private readonly ILogger<NotificationViewComponent> _logger;
        private readonly INotificationApiClient _notificationApiClient;

        public NotificationViewComponent(ILogger<NotificationViewComponent> logger,
            INotificationApiClient notificationApiClient)
        {
            _logger = logger;
            _notificationApiClient = notificationApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(NotificationModel model)
        {
            _logger.LogInformation("Get SetCookie ");
            var noti = await _notificationApiClient.GetNoti();

            var categories = new List<NotificationModel>();
            var data = noti;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new NotificationModel
                    {
                        CreatedDate = m.CreatedDate,
                        UserId = m.UserId,
                        Url = m.Url
                    };
                    categories.Add(item);
                }
            }

            _logger.LogInformation("End get SetCookie");
            return View(categories);
        }
    }
}