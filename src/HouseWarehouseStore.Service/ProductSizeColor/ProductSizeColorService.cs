using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public class ProductSizeColorService : IProductSizeColorService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public ProductSizeColorService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductSizeColorModel>> GetAll(string id)
        {
            var banner = from x in _context.ProductSizeColors
                         join c in _context.Colors on x.ColorId equals c.ColorId into pt
                         from tp in pt.DefaultIfEmpty()
                         join s in _context.Sizes on x.SizeId equals s.SizeId into ps
                         from sp in ps.DefaultIfEmpty()
                         where x.ProductId == id
                         select new ProductSizeColorModel()
                         {
                             SizeId = x.SizeId,
                             ProductId = x.ProductId,
                             ColorId = x.ColorId,
                             ColorName = tp.NameColor,
                             SizeName = sp.SizeProduct,
                         };

            return banner.ToList();
        }
    }
}