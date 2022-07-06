using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface IBannerService
    {
        Task<List<BannerModel>> GetAll(int Width, int Height);
    }
}