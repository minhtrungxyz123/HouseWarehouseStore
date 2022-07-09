using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class ProductService : IProductService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public ProductService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll(bool showHidden = true)
        {
            return await _context.Products
                             .OrderByDescending(p => p.Name).Where(c => c.Home == showHidden)
                             .ToListAsync();
        }
    }
}