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

        public async Task<ApiResult<Collection>> GetByIdAsyn(int? id)
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
                Products = item.Products,
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

        public async Task<ApiResult<Pagination<Collection>>> GetAllPaging(CollectionSearchContext ctx)
        {
            var query = _context.Collections.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.Name.Contains(ctx.Keyword)
                || x.Description.Contains(ctx.Keyword)
                || x.Body.Contains(ctx.Keyword)
                || x.Factory.Contains(ctx.Keyword)
                || x.TitleMeta.Contains(ctx.Keyword)
                || x.Content.Contains(ctx.Keyword)
                || x.BarCode.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Collection()
                {
                    Name = x.Name,
                    CollectionId = x.CollectionId,
                    Active = x.Active,
                    TitleMeta = x.TitleMeta,
                    Content = x.Content,
                    StatusProduct = x.StatusProduct,
                    Sort = x.Sort,
                    Quantity = x.Quantity,
                    BarCode = x.BarCode,
                    Price = x.Price,
                    Body = x.Body,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    Description = x.Description,
                    Factory = x.Factory,
                    Home = x.Home,
                    Hot = x.Hot,
                    Image = x.Image,
                }).ToListAsync();

            var pagedResult = new Pagination<Collection>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Collection>>(pagedResult);
        }

        public async Task<Collection?> GetById(int? id)
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
                query = from p in query where p.Active select p;
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

        public async Task<RepositoryResponse> Update(int? id, CollectionModel model)
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

        public async Task<int> Delete(int? id)
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