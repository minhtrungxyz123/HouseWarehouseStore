using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class TagService : ITagService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public TagService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<HouseWarehouseStore.Data.Entities.Tag>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Tags
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.TagId == id);

            var model = new HouseWarehouseStore.Data.Entities.Tag()
            {
                TagId = item.TagId,
                Name = item.Name,
                Active = item.Active,
                Soft = item.Soft
            };
            return new ApiSuccessResult<HouseWarehouseStore.Data.Entities.Tag>(model);
        }

        public async Task<IEnumerable<HouseWarehouseStore.Data.Entities.Tag>> GetAll()
        {
            return await _context.Tags
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<HouseWarehouseStore.Data.Entities.Tag>>> GetAllPaging(TagSearchContext ctx)
        {
            var query = _context.Tags.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.Name.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Tag()
                {
                    Name = x.Name,
                    TagId = x.TagId,
                    Active = x.Active,
                    Soft = x.Soft
                }).ToListAsync();

            var pagedResult = new Pagination<Tag>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Tag>>(pagedResult);
        }

        public async Task<Tag?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Tags
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.TagId == id);

            return item;
        }

        public IList<Tag> GetActive(bool showHidden = true)
        {
            var query = from p in _context.Tags.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(TagModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Tag item = new Tag()
            {
                Name = model.Name,
                Active = model.Active,
                Soft = model.Soft,
                TagId = Guid.NewGuid().ToString(),
            };

            await _context.Tags.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.TagId,
            };
        }

        public async Task<RepositoryResponse> Update(string? id, TagModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Tags.FindAsync(id);
            item.Name = model.Name;
            item.Active = model.Active;
            item.Soft = model.Soft;

            _context.Tags.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id,
            };
        }

        public async Task<int> Delete(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Tags.FindAsync(id);

            _context.Tags.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}