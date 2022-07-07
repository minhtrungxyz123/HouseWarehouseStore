using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseWarehouseStore.Service
{
    public class CommentService : ICommentService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public CommentService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAll()
        {
            return await _context.Comments
                             .OrderByDescending(p => p.CustomerName)
                             .ToListAsync();
        }
    }
}