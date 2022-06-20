using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class ProductService : IProductService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public ProductService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Product>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Products
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ProductId == id);

            var model = new Product()
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
                DescriptionMeta = item.DescriptionMeta,
                ProductId = item.ProductId,
                ProductCategorieId = item.ProductCategorieId,
                GiftInfo = item.GiftInfo,
                QuyCach = item.QuyCach,
                SaleOff = item.SaleOff
            };
            return new ApiSuccessResult<Product>(model);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<ProductModel>>> GetAllPaging(ProductSearchContext ctx)
        {
            var query = from pr in _context.Products
                        join c in _context.ProductCategories on pr.ProductCategorieId equals c.ProductCategorieId into pt
                        from tp in pt.DefaultIfEmpty()
                        join w in _context.Collections on pr.CollectionId equals w.CollectionId into wt
                        from tw in wt.DefaultIfEmpty()
                        select new { pr, tp, tw };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.Name.Contains(ctx.Keyword)
                || x.pr.TitleMeta.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new ProductModel()
                {
                    Name = u.pr.Name,
                    CollectionId = u.tw.Name,
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
                    SaleOff = u.pr.SaleOff,
                    QuyCach = u.pr.QuyCach,
                    GiftInfo = u.pr.GiftInfo,
                    ProductCategorieId = u.tp.Name,
                    ProductId = u.pr.ProductId,
                    DescriptionMeta = u.pr.DescriptionMeta
                })
                .ToListAsync();

            var pagination = new Pagination<ProductModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<ProductModel>>(pagination);
        }

        public async Task<Product?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Products
                            .OrderByDescending(p => p.Name)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ProductId == id);

            return item;
        }

        public IList<Product> GetActive(bool showHidden = true)
        {
            var query = from p in _context.Products.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        public IList<Product> GetHome(bool showHidden = true)
        {
            var query = from p in _context.Products.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Home select p;
            }
            query = from p in query orderby p.Name select p;
            return query.ToList();
        }

        public IList<Product> GetHot(bool showHidden = true)
        {
            var query = from p in _context.Products.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Hot select p;
            }
            query = from p in query orderby p.Hot select p;
            return query.ToList();
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(ProductModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Product item = new Product()
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
                ProductId = model.ProductId,
                DescriptionMeta = model.DescriptionMeta,
                ProductCategorieId = model.ProductCategorieId,
                GiftInfo = model.GiftInfo,
                QuyCach = model.QuyCach,
                SaleOff = model.SaleOff
            };

            await _context.Products.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.ProductId
            };
        }

        public async Task<RepositoryResponse> Update(string? id, ProductModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Products.FindAsync(id);
            item.CollectionId = model.CollectionId;
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
            item.DescriptionMeta = model.DescriptionMeta;
            item.ProductCategorieId = model.ProductCategorieId;
            item.GiftInfo = model.GiftInfo;
            item.QuyCach = model.QuyCach;
            item.SaleOff = model.SaleOff;

            _context.Products.Update(item);

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

            var item = await _context.Products.FindAsync(id);

            _context.Products.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public IList<Product> GetStatusProduct(bool showHidden = true)
        {
            var query = from p in _context.Products.AsQueryable() select p;
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