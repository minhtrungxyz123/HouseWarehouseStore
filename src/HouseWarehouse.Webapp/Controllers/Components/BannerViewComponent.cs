using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouse.Webapp.Models;
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
            var viewModel = new BannerViewModel
            {
                BannerModels = await _bannerApiClient.GetAll()
            };

            return View(viewModel);
        }
    }
}