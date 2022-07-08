using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
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

        public async Task<List<CommentModel>> GetByIdAsyn(string id)
        {
            var banner = from x in _context.Comments
                         where x.ProductId == id
                         select new CommentModel()
                         {
                             CommentId = x.CommentId,
                             CustomerName = x.CustomerName,
                             Contents = x.Contents,
                             Image = x.Image,
                             ProductId = x.ProductId,
                             Profession = x.Profession,
                             Star = x.Star
                         };

            return banner.ToList();
        }
    }
}