using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    public class CommentViewComponent : ViewComponent
    {
        private readonly ICommentApiClient _commentApiClient;

        public CommentViewComponent(ICommentApiClient commentApiClient)
        {
            _commentApiClient = commentApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = await _commentApiClient.GetAll();

            var categories = new List<CommentModel>();
            var data = banner;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new CommentModel
                    {
                        ProductId = m.ProductId,
                        CustomerName = m.CustomerName,
                        CommentId = m.CommentId,
                        Contents = m.Contents,
                        Image = m.Image,
                        Profession = m.Profession,
                        Star = m.Star
                    };
                    categories.Add(item);
                }
            }

            return View(categories);
        }
    }
}