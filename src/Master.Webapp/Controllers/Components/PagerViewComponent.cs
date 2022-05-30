﻿using HouseWarehouseStore.Common;
using Microsoft.AspNetCore.Mvc;

namespace Master.Webapp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PaginationBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}