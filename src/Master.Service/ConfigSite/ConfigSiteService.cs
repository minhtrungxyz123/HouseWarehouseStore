using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class ConfigSiteService : IConfigSiteService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public ConfigSiteService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<ConfigSite>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ConfigSites
                            .OrderByDescending(p => p.Description)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ConfigSiteId == id);

            var model = new ConfigSite()
            {
                Description = item.Description,
                ConfigSiteId = item.ConfigSiteId,
                ContactInfo = item.ContactInfo,
                CoverImage = item.CoverImage,
                Email = item.Email,
                Facebook = item.Facebook,
                FbMessage = item.FbMessage,
                FooterInfo = item.FooterInfo,
                GoogleAnalytics = item.GoogleAnalytics,
                GoogleMap = item.GoogleMap,
                GooglePlus = item.GooglePlus,
                Hotline = item.Hotline,
                Linkedin = item.Linkedin,
                LiveChat = item.LiveChat,
                NameShopee = item.NameShopee,
                SaleOffProgram = item.SaleOffProgram,
                Title = item.Title,
                Twitter = item.Twitter,
                UrlWeb = item.UrlWeb,
                Youtube = item.Youtube
            };
            return new ApiSuccessResult<ConfigSite>(model);
        }

        public async Task<IEnumerable<ConfigSite>> GetAll()
        {
            return await _context.ConfigSites
                            .OrderByDescending(p => p.Description)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<ConfigSiteModel>>> GetAllPaging(ConfigSiteSearchContext ctx)
        {
            var query = from pr in _context.ConfigSites
                        select new { pr };

            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.pr.Description.Contains(ctx.Keyword)
                || x.pr.Email.Contains(ctx.Keyword));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(u => new ConfigSiteModel()
                {
                    Description = u.pr.Description,
                    Email = u.pr.Email,
                    Youtube = u.pr.Youtube,
                    Twitter = u.pr.Twitter,
                    Title = u.pr.Title,
                    ConfigSiteId = u.pr.ConfigSiteId,
                    ContactInfo = u.pr.ContactInfo,
                    CoverImage = u.pr.CoverImage,
                    Facebook = u.pr.Facebook,
                    FbMessage = u.pr.FbMessage,
                    FooterInfo = u.pr.FooterInfo,
                    GoogleAnalytics = u.pr.GoogleAnalytics,
                    GoogleMap = u.pr.GoogleMap,
                    GooglePlus = u.pr.GooglePlus,
                    Hotline = u.pr.Hotline,
                    Linkedin = u.pr.Linkedin,
                    LiveChat = u.pr.LiveChat,
                    nameShopee = u.pr.NameShopee,
                    SaleOffProgram = u.pr.SaleOffProgram,
                    urlWeb = u.pr.UrlWeb,
                })
                .ToListAsync();

            var pagination = new Pagination<ConfigSiteModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
            };

            return new ApiSuccessResult<Pagination<ConfigSiteModel>>(pagination);
        }

        public async Task<ConfigSite?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.ConfigSites
                            .OrderByDescending(p => p.Description)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ConfigSiteId == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(ConfigSiteModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ConfigSite item = new ConfigSite()
            {
                ConfigSiteId = model.ConfigSiteId,
                Description = model.Description,
                UrlWeb = model.urlWeb,
                SaleOffProgram = model.SaleOffProgram,
                NameShopee = model.nameShopee,
                LiveChat = model.LiveChat,
                ContactInfo = model.ContactInfo,
                CoverImage = model.CoverImage,
                Email = model.Email,
                Facebook = model.Facebook,
                FbMessage = model.FbMessage,
                FooterInfo = model.FooterInfo,
                GoogleAnalytics = model.GoogleAnalytics,
                GoogleMap = model.GoogleMap,
                GooglePlus = model.GooglePlus,
                Hotline = model.Hotline,
                Linkedin = model.Linkedin,
                Title = model.Title,
                Twitter = model.Twitter,
                Youtube = model.Youtube
            };

            await _context.ConfigSites.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.ConfigSiteId
            };
        }

        public async Task<RepositoryResponse> Update(string? id, ConfigSiteModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.ConfigSites.FindAsync(id);
            item.Description = model.Description;
            item.UrlWeb = model.urlWeb;
            item.SaleOffProgram = model.SaleOffProgram;
            item.NameShopee = model.nameShopee;
            item.LiveChat = model.LiveChat;
            item.ContactInfo = model.ContactInfo;
            item.CoverImage = model.CoverImage;
            item.Email = model.Email;
            item.Facebook = model.Facebook;
            item.FbMessage = model.FbMessage;
            item.FooterInfo = model.FooterInfo;
            item.GoogleAnalytics = model.GoogleAnalytics;
            item.GoogleMap = model.GoogleMap;
            item.GooglePlus = model.GooglePlus;
            item.Hotline = model.Hotline;
            item.Linkedin = model.Linkedin;
            item.Title = model.Title;
            item.Twitter = model.Twitter;
            item.Youtube = model.Youtube;

            _context.ConfigSites.Update(item);

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

            var item = await _context.ConfigSites.FindAsync(id);

            _context.ConfigSites.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}