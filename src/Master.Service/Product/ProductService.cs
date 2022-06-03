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

        public async Task<ApiResult<Product>> GetByIdAsyn(int? id)
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
                ProductId = item.ProductId,
                CollectionId = item.CollectionId,
                BarCode = item.BarCode,
                Content = item.Content,
                Body = item.Body,
                Carts = item.Carts,
                Collection = item.Collection,
                CreateBy = item.CreateBy,
                CreateDate = item.CreateDate,
                Description = item.Description,
                DescriptionMeta = item.DescriptionMeta,
                Factory = item.Factory,
                GiftInfo = item.GiftInfo,
                Home = item.Home,
                Hot = item.Hot,
                Image = item.Image,
                OrderDetails = item.OrderDetails,
                Price = item.Price,
                ProductCategorie = item.ProductCategorie,
                ProductCategorieId = item.ProductCategorieId,
                ProductLikes = item.ProductLikes,
                ProductSizeColors = item.ProductSizeColors,
                Quantity = item.Quantity,
                QuyCach = item.QuyCach,
                ReviewProducts = item.ReviewProducts,
                SaleOff = item.SaleOff,
                Sort = item.Sort,
                StatusProduct = item.StatusProduct,
                Tags = item.Tags,
                TitleMeta = item.TitleMeta,
            };
            return new ApiSuccessResult<Product>(model);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                            .OrderByDescending(p => p.Name)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Product>>> Get(ProductSearchContext ctx)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.Name.Contains(ctx.Keyword) || x.Name.Contains(ctx.Keyword) || x.Content.Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Product()
                {
                    Active = x.Active,
                    Description = x.Description,
                    TitleMeta = x.TitleMeta,
                    StatusProduct = x.StatusProduct,
                    Sort = x.Sort,
                    BarCode = x.BarCode,
                    Body = x.Body,
                    CollectionId = x.CollectionId,
                    Content = x.Content,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    DescriptionMeta = x.DescriptionMeta,
                    Factory = x.Factory,
                    GiftInfo = x.GiftInfo,
                    Home = x.Home,
                    Hot = x.Hot,
                    Image = x.Image,
                    Name = x.Name,
                    Price = x.Price,
                    ProductCategorieId = x.ProductCategorieId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    QuyCach = x.QuyCach,
                    SaleOff = x.SaleOff

                }).ToListAsync();

            var pagedResult = new Pagination<Product>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Product>>(pagedResult);
        }

        public async Task<Product?> GetById(int? id)
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

        public IList<Product> GetMvcListItems(bool showHidden = true)
        {
            var query = from p in _context.Products.AsQueryable() select p;
            if (showHidden)
            {
                query = from p in query where p.Active select p;
            }
            query = from p in query orderby p.Name select p;
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
                Active = model.Active,
                Description = model.Description,
                TitleMeta = model.TitleMeta,
                StatusProduct = model.StatusProduct,
                Sort = model.Sort,
                BarCode = model.BarCode,
                Body = model.Body,
                CollectionId = model.CollectionId,
                Content = model.Content,
                CreateBy = model.CreateBy,
                CreateDate = model.CreateDate,
                DescriptionMeta = model.DescriptionMeta,
                Factory = model.Factory,
                GiftInfo = model.GiftInfo,
                Home = model.Home,
                Hot = model.Hot,
                Image = model.Image,
                Name = model.Name,
                Price = model.Price,
                ProductCategorieId = model.ProductCategorieId,
                Quantity = model.Quantity,
                QuyCach = model.QuyCach,
                SaleOff = model.SaleOff
            };

            await _context.Products.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.ProductId.ToString(),
            };
        }

        public async Task<RepositoryResponse> Update(int? id, ProductModel model)
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

            item.Active = model.Active;
            item.Description = model.Description;
            item.TitleMeta = model.TitleMeta;
            item.StatusProduct = model.StatusProduct;
            item.Sort = model.Sort;
            item.BarCode = model.BarCode;
            item.Body = model.Body;
            item.CollectionId = model.CollectionId;
            item.Content = model.Content;
            item.CreateBy = model.CreateBy;
            item.CreateDate = model.CreateDate;
            item.DescriptionMeta = model.DescriptionMeta;
            item.Factory = model.Factory;
            item.GiftInfo = model.GiftInfo;
            item.Home = model.Home;
            item.Hot = model.Hot;
            item.Image = model.Image;
            item.Name = model.Name;
            item.Price = model.Price;
            item.ProductCategorieId = model.ProductCategorieId;
            item.Quantity = model.Quantity;
            item.QuyCach = model.QuyCach;
            item.SaleOff = model.SaleOff;

            _context.Products.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id.ToString(),
            };
        }

        public async Task<int> Delete(int? id)
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

        #endregion Method
    }
}