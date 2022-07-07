using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class FollowService : IFollowService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public FollowService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Follow>> GetByIdAsyn(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Follows
                            .OrderByDescending(p => p.Youtube)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.FollowId == id);

            var model = new Follow()
            {
                FollowId = item.FollowId,
                Youtube = item.Youtube,
                Icon = item.Icon,
                Facebook = item.Facebook,
                Instagram = item.Instagram,
                Linkedin = item.Linkedin,
                Twitter = item.Twitter
            };
            return new ApiSuccessResult<Follow>(model);
        }

        public async Task<ApiResult<Pagination<Follow>>> GetAllPaging(FollowSearchContext ctx)
        {
            var query = _context.Follows.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.Youtube.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Follow()
                {
                    FollowId = x.FollowId,
                    Youtube = x.Youtube,
                    Twitter = x.Twitter,
                    Linkedin = x.Linkedin,
                    Instagram = x.Instagram,
                    Facebook = x.Facebook,
                    Icon = x.Icon
                }).ToListAsync();

            var pagedResult = new Pagination<Follow>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Follow>>(pagedResult);
        }

        public async Task<Follow?> GetById(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Follows
                            .OrderByDescending(p => p.Youtube)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.FollowId == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(FollowModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Follow item = new Follow()
            {
                Youtube = model.Youtube,
                FollowId = Guid.NewGuid().ToString(),
                Icon = model.Icon,
                Facebook = model.Facebook,
                Instagram = model.Facebook,
                Linkedin = model.Linkedin,
                Twitter = model.Twitter
            };

            await _context.Follows.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.FollowId
            };
        }

        public async Task<RepositoryResponse> Update(string id, FollowModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Follows.FindAsync(id);
            item.Youtube = model.Youtube;
            item.Icon = model.Icon;
            item.Linkedin = model.Linkedin;
            item.Twitter = model.Twitter;
            model.Facebook = model.Facebook;
            model.Instagram = model.Instagram;

            _context.Follows.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Follows.FindAsync(id);

            _context.Follows.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}