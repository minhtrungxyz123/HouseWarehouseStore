using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public CategoryService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductCategory>> GetAll(bool showHidden = true)
        {
            return await _context.ProductCategories
                             .OrderByDescending(p => p.Name).Where(p => p.Active == showHidden)
                             .ToListAsync();
        }
    }
}