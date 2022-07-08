using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class FollowService : IFollowService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public FollowService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Follow>> GetAll()
        {
            return await _context.Follows
                             .OrderByDescending(p => p.FollowLink)
                             .ToListAsync();
        }
    }
}