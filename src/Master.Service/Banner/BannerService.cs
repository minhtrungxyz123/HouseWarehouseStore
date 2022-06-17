using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class BannerService : IBannerService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public BannerService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Banner>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Banners
                            .OrderByDescending(p => p.BannerName)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.BannerId == id);

            var model = new Banner()
            {
                BannerName = item.BannerName,
                Active = item.Active,
                BannerId = item.BannerId,
                Content = item.Content,
                CoverImage = item.CoverImage,
                GroupId = item.GroupId,
                Height = item.Height,
                Soft = item.Soft,
                Title = item.Title,
                Url = item.Url,
                Width = item.Width
            };
            return new ApiSuccessResult<Banner>(model);
        }

        public async Task<IEnumerable<Banner>> GetAll()
        {
            return await _context.Banners
                            .OrderByDescending(p => p.BannerName)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<BannerModel>>> GetAllPaging(BannerSearchContext ctx)
        {
            var query = from pr in _context.Banners
                        select new { pr };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.BannerName.Contains(ctx.Keyword)
                || x.pr.Url.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new BannerModel()
                {
                    BannerName = u.pr.BannerName,
                    GroupId = u.pr.GroupId,
                    Active = u.pr.Active,
                    Content = u.pr.Content,
                    Url = u.pr.Url,
                    Width = u.pr.Width,
                    Height = u.pr.Height,
                    BannerId = u.pr.BannerId,
                    CoverImage = u.pr.CoverImage,
                    Soft = u.pr.Soft,
                    Title = u.pr.Title
                })
                .ToListAsync();

            var pagination = new Pagination<BannerModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<BannerModel>>(pagination);
        }

        public async Task<Banner?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Banners
                            .OrderByDescending(p => p.BannerName)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.BannerId == id);

            return item;
        }

        public IList<Banner> GetActive(bool showHidden = true)
        {
            var query = from p in _context.Banners.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.BannerName select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(BannerModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Banner item = new Banner()
            {
                BannerId = model.BannerId,
                BannerName = model.BannerName,
                Active = model.Active,
                Title = model.Title,
                Width = model.Width,
                Soft = model.Soft,
                CoverImage = model.CoverImage,
                Content = model.Content,
                GroupId = model.GroupId,
                Height = model.Height,
                Url = model.Url
            };

            await _context.Banners.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.BannerId
            };
        }

        public async Task<RepositoryResponse> Update(string? id, BannerModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Banners.FindAsync(id);
            item.BannerName = model.BannerName;
            item.Active = model.Active;
            item.Title = model.Title;
            item.Width = model.Width;
            item.Soft = model.Soft;
            item.CoverImage = model.CoverImage;
            item.Content = model.Content;
            item.GroupId = model.GroupId;
            item.Height = model.Height;
            item.Url = model.Url;

            _context.Banners.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };
        }

        public async Task<int> Delete(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Banners.FindAsync(id);

            _context.Banners.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}