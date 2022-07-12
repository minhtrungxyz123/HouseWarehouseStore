using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    public class ReviewProductViewComponent : ViewComponent
    {
        public ReviewProductViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new CommentModel();
            return View(model);
        }
    }
}