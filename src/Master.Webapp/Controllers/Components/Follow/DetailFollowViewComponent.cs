using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Follow
{
    public class DetailFollowViewComponent : ViewComponent
    {
        public DetailFollowViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(FollowModel view)
        {
            var model = new FollowModel();
            model.FollowId = view.FollowId;
            model.Twitter = view.Twitter;
            model.Linkedin = view.Linkedin;
            model.Youtube = view.Youtube;
            model.Facebook = view.Facebook;
            model.Icon = view.Icon;
            model.Instagram = view.Instagram;
            return View(model);
        }
    }
}