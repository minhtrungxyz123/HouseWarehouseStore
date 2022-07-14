using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public class SizeService : ISizeService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public SizeService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<SizeModel>> GetAll(string id)
        {
            var banner = from x in _context.Sizes
                         where x.SizeId == id
                         select new SizeModel()
                         {
                             SizeId = x.SizeId,
                             SizeProduct = x.SizeProduct,
                         };

            return banner.ToList();
        }
    }
}