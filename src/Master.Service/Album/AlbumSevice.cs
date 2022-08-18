using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class AlbumSevice : IAlbumSevice
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public AlbumSevice(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Album>> GetByIdAsyn(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Albums
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.AlbumId == id);

            var model = new Album()
            {
                Name = item.Name,
                Active = item.Active,
                AlbumId = item.AlbumId,
                Body = item.Body,
                CreateDate = item.CreateDate,
                Description = item.Description,
                Home = item.Home,
                ListImage = item.ListImage,
                Sort = item.Sort,
                Title = item.Title
            };
            return new ApiSuccessResult<Album>(model);
        }

        public async Task<IEnumerable<Album>> GetAll()
        {
            return await _context.Albums
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<AlbumModel>>> GetAllPaging(AlbumSearchContext ctx)
        {
            var query = from pr in _context.Albums
                        select new { pr };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.Name.Contains(ctx.Keyword)
                || x.pr.Name.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new AlbumModel()
                {
                    Name = u.pr.Name,
                    Title = u.pr.Title,
                    Sort = u.pr.Sort,
                    ListImage = u.pr.ListImage,
                    Home = u.pr.Home,
                    Description = u.pr.Description,
                    Active = u.pr.Active,
                    AlbumId = u.pr.AlbumId,
                    Body = u.pr.Body,
                    CreateDate = u.pr.CreateDate
                })
                .ToListAsync();

            var pagination = new Pagination<AlbumModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<AlbumModel>>(pagination);
        }

        public async Task<Album?> GetById(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Albums
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.AlbumId == id);

            return item;
        }

        public IList<Album> GetActive(bool showHidden = true)
        {
            var query = from p in _context.Albums.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        public IList<Album> GetHome(bool showHidden = true)
        {
            var query = from p in _context.Albums.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Home select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(AlbumModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Album item = new Album()
            {
                AlbumId = model.AlbumId,
                Name = model.Name,
                Active = model.Active,
                CreateDate = model.CreateDate,
                Body = model.Body,
                Description = model.Description,
                Home = model.Home,
                ListImage = model.ListImage,
                Sort = model.Sort,
                Title = model.Title
            };

            await _context.Albums.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.AlbumId
            };
        }

        public async Task<RepositoryResponse> Update(string id, AlbumModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Albums.FindAsync(id);
            item.Name = model.Name;
            item.Active = model.Active;
            item.Body = model.Body;
            item.Home = model.Home;
            item.ListImage = model.ListImage;
            item.Sort = model.Sort;
            item.Title = model.Title;
            model.Active = model.Active;
            model.CreateDate = model.CreateDate;
            model.Description = model.Description;

            _context.Albums.Update(item);

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

            var item = await _context.Albums.FindAsync(id);

            _context.Albums.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}