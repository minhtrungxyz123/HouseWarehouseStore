using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouse.Webapp.Models;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly IBannerApiClient _bannerApiClient;

        public BannerViewComponent(IBannerApiClient bannerApiClient)
        {
            _bannerApiClient = bannerApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = await _bannerApiClient.GetAll();

            var categories = new List<BannerModel>();
            var data = banner;

            if (data?.Count > 0)
            {
                foreach (var m in data)
                {
                    var item = new BannerModel
                    {
                        BannerId = m.BannerId,
                        Active = m.Active,
                        BannerName = m.BannerName,
                        Content = m.Content,
                        CoverImage = m.CoverImage,
                        GroupId = m.GroupId,
                        Height = m.Height,
                        Soft = m.Soft,
                        Title = m.Title,
                        Url = m.Url,
                        Width = m.Width
                    };
                    categories.Add(item);
                }
            }

            return View(categories);
        }
    }
}