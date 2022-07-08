using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Follow
{
    public class CreateFollowViewComponent : ViewComponent
    {
        public CreateFollowViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new FollowModel();
            return View(model);
        }
    }
}