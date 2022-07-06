using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class BannerService : IBannerService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public BannerService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<BannerModel>> GetAll()
        {
            var banner = await _context.Banners.OrderBy(x => x.BannerName)
                .Select(x => new BannerModel()
                {
                    BannerId = x.BannerId,
                    Url = x.Url,
                    BannerName = x.BannerName,
                    Active = x.Active,
                    Content = x.Content,
                    CoverImage = x.CoverImage,
                    GroupId = x.GroupId,
                    Height = x.Height,
                    Soft = x.Soft,
                    Title = x.Title,
                    Width = x.Width
                }).ToListAsync();

            return banner;
        }

        //public async Task<List<BannerModel>> GetAll(int Width, int Height)
        //{
        //    var banner = from x in _context.Banners
        //                 where x.Width == Width && x.Height == Height
        //                 select new BannerModel()
        //                 {
        //                     BannerId = x.BannerId,
        //                     Url = x.Url,
        //                     BannerName = x.BannerName,
        //                     Active = x.Active,
        //                     Content = x.Content,
        //                     CoverImage = x.CoverImage,
        //                     GroupId = x.GroupId,
        //                     Height = x.Height,
        //                     Soft = x.Soft,
        //                     Title = x.Title,
        //                     Width = x.Width
        //                 };

        //    return banner.ToList();
        //}
    }
}