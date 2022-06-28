using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Files.Service
{
    public class FilesService : IFilesService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public FilesService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region Method

        public async Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string collectionId)
        {
            if (collectionId is null)
            {
                throw new ArgumentNullException(nameof(collectionId));
            }

            var list = new List<HouseWarehouseStore.Data.Entities.File>();
            foreach (var item in entities)
            {
                var files = new HouseWarehouseStore.Data.Entities.File()
                {
                    FileName = item.FileName,
                    Extension = item.Extension,
                    MimeType = item.MimeType,
                    Path = item.Path,
                    Size = item.Size,
                    CollectionId = collectionId,
                };

                files.Id = Guid.NewGuid().ToString();
                list.Add(files);
            }

            await _context.Files.AddRangeAsync(list);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string collectionId)
        {
            if (collectionId is null)
            {
                throw new ArgumentNullException(nameof(collectionId));
            }

            var itemFiles = await _context.Files.FirstOrDefaultAsync(x => x.CollectionId.Equals(collectionId));
            var list = new List<HouseWarehouseStore.Data.Entities.File>();
            foreach (var file in entities)
            {
                itemFiles.FileName = file.FileName;
                itemFiles.Extension = file.Extension;
                itemFiles.MimeType = file.MimeType;
                itemFiles.Path = file.Path;
                itemFiles.Size = file.Size;
                file.Id = itemFiles.Id;
                file.CollectionId = itemFiles.CollectionId;
            }

            list.Add(itemFiles);
            _context.Files.Update(itemFiles);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> Delete(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Files.FirstOrDefaultAsync(x => x.CollectionId.Equals(id));

            _context.Files.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method

        #region List

        public async Task<ApiResult<Pagination<FilesModel>>> GetAllPaging(FileSearchContext ctx)
        {
            var query = from pr in _context.Files
                        join c in _context.Collections on pr.CollectionId equals c.CollectionId into pt
                        from tp in pt.DefaultIfEmpty()
                        join w in _context.Articles on pr.ArticleId equals w.Id into wt
                        from tw in wt.DefaultIfEmpty()
                        join f in _context.ConfigSites on pr.ConfigSiteId equals f.ConfigSiteId into ft
                        from tf in ft.DefaultIfEmpty()
                        join b in _context.Banners on pr.BannerId equals b.BannerId into bt
                        from tb in bt.DefaultIfEmpty()
                        join r in _context.ProductCategories on pr.ProductCategoryId equals r.ProductCategorieId into rt
                        from tr in rt.DefaultIfEmpty()
                        join u in _context.Products on pr.ProductId equals u.ProductId into ut
                        from tu in ut.DefaultIfEmpty()
                        join a in _context.Admins on pr.AdminId equals a.Id into at
                        from ta in at.DefaultIfEmpty()
                        select new { pr, tp, tw, tf, tr, tu, ta };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.FileName.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new FilesModel()
                {
                    FileName = u.pr.FileName,
                    Id = u.pr.Id,
                    AdminId = u.pr.AdminId,
                    ProductId = u.pr.ProductId,
                    ArticlesId = u.pr.ArticleId,
                    BannerId = u.pr.BannerId,
                    CollectionId = u.pr.CollectionId,
                    ConfigsiteId = u.pr.ConfigSiteId,
                    Extension = u.pr.Extension,
                    MimeType = u.pr.MimeType,
                    Path = u.pr.Path,
                    ProductCategoryId = u.pr.ProductCategoryId,
                    Size = u.pr.Size
                })
                .ToListAsync();

            var pagination = new Pagination<FilesModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<FilesModel>>(pagination);
        }

        public async Task<List<FilesModel>> GetFilesCollection(int take)
        {
            //1. Select join
            var query = from p in _context.Files
                        select new { p };

            var data = await query.OrderByDescending(x => x.p.FileName).Take(take)
                .Select(x => new FilesModel()
                {
                    Id = x.p.Id,
                    FileName = x.p.FileName,
                    CollectionId = x.p.CollectionId,
                    Size = x.p.Size,
                    Extension = x.p.Extension,
                    MimeType = x.p.MimeType,
                    Path = x.p.Path
                }).ToListAsync();

            return data;
        }

        public async Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var query =
                from x in _context.Files
                where x.CollectionId == id
                select x;

            return await query.FirstOrDefaultAsync();
        }

        public Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var query =
                from x in _context.Files
                where x.CollectionId == name
                select x;

            return query.FirstOrDefaultAsync();
        }

        #endregion List
    }
}