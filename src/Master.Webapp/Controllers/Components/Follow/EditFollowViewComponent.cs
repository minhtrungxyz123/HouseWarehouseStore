﻿using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components.Follow
{
    public class EditFollowViewComponent : ViewComponent
    {
        public EditFollowViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(FollowModel view)
        {
            var model = new FollowModel();
            model.FollowId = view.FollowId;
            model.FollowLink = view.FollowLink;
            model.Icon = view.Icon;
            return View(model);
        }
    }
}