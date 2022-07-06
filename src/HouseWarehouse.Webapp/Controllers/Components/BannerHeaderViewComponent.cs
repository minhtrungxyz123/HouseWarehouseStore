using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouseStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Webapp.Controllers.Components
{
    public class BannerHeaderViewComponent : ViewComponent
    {
        private readonly IBannerApiClient _bannerApiClient;

        public BannerHeaderViewComponent(IBannerApiClient bannerApiClient)
        {
            _bannerApiClient = bannerApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(int Width = 400, int Height = 300)
        {
            var banner = await _bannerApiClient.GetAll(Width, Height);

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