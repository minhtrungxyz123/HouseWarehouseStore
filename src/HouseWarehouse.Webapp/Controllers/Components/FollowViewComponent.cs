using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    public class FollowViewComponent : ViewComponent
    {
        private readonly IFollowApiClient _followApiClient;

        public FollowViewComponent(IFollowApiClient followApiClient)
        {
            _followApiClient = followApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = await _followApiClient.GetAll();

            var categories = new List<FollowModel>();
            var data = banner;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new FollowModel
                    {
                        FollowId = m.FollowId,
                        Youtube = m.Youtube,
                        Facebook = m.Facebook,
                        Icon = m.Icon,
                        Instagram = m.Instagram,
                        Linkedin = m.Linkedin,
                        Twitter = m.Twitter
                    };
                    categories.Add(item);
                }
            }

            return View(categories);
        }
    }
}