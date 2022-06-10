using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class CollectionService : ICollectionService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public CollectionService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Collection>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Collections
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.CollectionId == id);

            var model = new Collection()
            {
                Name = item.Name,
                Active = item.Active,
                CollectionId = item.CollectionId,
                BarCode = item.BarCode,
                Body = item.Body,
                Content = item.Content,
                CreateBy = item.CreateBy,
                CreateDate = item.CreateDate,
                Description = item.Description,
                Factory = item.Factory,
                Home = item.Home,
                Hot = item.Hot,
                Image = item.Image,
                Price = item.Price,
                Quantity = item.Quantity,
                Sort = item.Sort,
                StatusProduct = item.StatusProduct,
                TitleMeta = item.TitleMeta,
            };
            return new ApiSuccessResult<Collection>(model);
        }

        public async Task<IEnumerable<Collection>> GetAll()
        {
            return await _context.Collections
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<CollectionModel>>> GetAllPaging(CollectionSearchContext ctx)
        {
            var query = from pr in _context.Collections
                        select new { pr };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.Name.Contains(ctx.Keyword)
                || x.pr.TitleMeta.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new CollectionModel()
                {
                    Name = u.pr.Name,
                    CollectionId = u.pr.CollectionId,
                    Active = u.pr.Active,
                    TitleMeta = u.pr.TitleMeta,
                    Content = u.pr.Content,
                    StatusProduct = u.pr.StatusProduct,
                    Sort = u.pr.Sort,
                    Quantity = u.pr.Quantity,
                    BarCode = u.pr.BarCode,
                    Price = u.pr.Price,
                    Body = u.pr.Body,
                    CreateBy = u.pr.CreateBy,
                    CreateDate = u.pr.CreateDate,
                    Description = u.pr.Description,
                    Factory = u.pr.Factory,
                    Home = u.pr.Home,
                    Hot = u.pr.Hot,
                    Image = u.pr.Image,
                })
                .ToListAsync();

            var pagination = new Pagination<CollectionModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<CollectionModel>>(pagination);
        }

        public async Task<Collection?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Collections
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.CollectionId == id);

            return item;
        }

        public IList<Collection> GetActive(bool showHidden = true)
        {
            var query = from p in _context.Collections.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        public IList<Collection> GetHome(bool showHidden = true)
        {
            var query = from p in _context.Collections.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Home select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        public IList<Collection> GetHot(bool showHidden = true)
        {
            var query = from p in _context.Collections.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Hot select p;
            }
            query = from p in query orderby p.Hot select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(CollectionModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Collection item = new Collection()
            {
                CollectionId = model.CollectionId,
                Name = model.Name,
                Active = model.Active,
                BarCode = model.BarCode,
                Body = model.Body,
                Content = model.Content,
                CreateBy = model.CreateBy,
                CreateDate = model.CreateDate,
                Description = model.Description,
                Factory = model.Factory,
                Home = model.Home,
                Hot = model.Hot,
                Image = model.Image,
                Price = model.Price,
                Quantity = model.Quantity,
                Sort = model.Sort,
                StatusProduct = model.StatusProduct,
                TitleMeta = model.TitleMeta,
            };

            await _context.Collections.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.CollectionId
            };
        }

        public async Task<RepositoryResponse> Update(string? id, CollectionModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Collections.FindAsync(id);
            item.Name = model.Name;
            item.Active = model.Active;
            item.BarCode = model.BarCode;
            item.Body = model.Body;
            item.Content = model.Content;
            item.CreateBy = model.CreateBy;
            item.CreateDate = model.CreateDate;
            item.Description = model.Description;
            item.Factory = model.Factory;
            item.Home = model.Home;
            item.Hot = model.Hot;
            item.Image = model.Image;
            item.Price = model.Price;
            item.Quantity = model.Quantity;
            item.Sort = model.Sort;
            item.StatusProduct = model.StatusProduct;
            item.TitleMeta = model.TitleMeta;

            _context.Collections.Update(item);

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

            var item = await _context.Collections.FindAsync(id);

            _context.Collections.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public IList<Collection> GetStatusProduct(bool showHidden = true)
        {
            var query = from p in _context.Collections.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.StatusProduct select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        #endregion Method
    }
}