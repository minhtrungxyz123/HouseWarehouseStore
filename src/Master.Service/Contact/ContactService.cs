using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Master.Service
{
    public class ContactService : IContactService
    {
        #region Fields

        private readonly HouseWarehouseStoreDbContext _context;

        public ContactService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        #endregion Fields

        #region List

        public async Task<ApiResult<Contact>> GetByIdAsyn(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Contacts
                            .OrderByDescending(p => p.Fullname)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ContactId == id);

            var model = new Contact()
            {
                Fullname = item.Fullname,
                ContactId = item.ContactId,
                Address = item.Address,
                Body = item.Body,
                CreateDate = item.CreateDate,
                Email = item.Email,
                Mobile = item.Mobile,
                Subject = item.Subject,
            };
            return new ApiSuccessResult<Contact>(model);
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _context.Contacts
                            .OrderByDescending(p => p.Fullname)
                            .ToListAsync();
        }

        public async Task<ApiResult<Pagination<Contact>>> GetAllPaging(ContactSearchContext ctx)
        {
            var query = _context.Contacts.AsQueryable();
            if (!string.IsNullOrEmpty(ctx.Keyword))
            {
                query = query.Where(x => x.Fullname.Contains(ctx.Keyword)
                || x.Address.Contains(ctx.Keyword)
                || x.Email.Contains(ctx.Keyword)
                || x.Subject.Contains(ctx.Keyword)
                || x.Body.Contains(ctx.Keyword)
                || x.Mobile.ToString().Contains(ctx.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((ctx.PageIndex - 1) * ctx.PageSize)
                .Take(ctx.PageSize)
                .Select(x => new Contact()
                {
                    Fullname = x.Fullname,
                    ContactId = x.ContactId,
                    Address = x.Address,
                    Email = x.Email,
                    Subject = x.Subject,
                    Body = x.Body,
                    Mobile = x.Mobile,
                    CreateDate = x.CreateDate,
                }).ToListAsync();

            var pagedResult = new Pagination<Contact>()
            {
                TotalRecords = totalRow,
                PageIndex = ctx.PageIndex,
                PageSize = ctx.PageSize,
                Items = data
            };
            return new ApiSuccessResult<Pagination<Contact>>(pagedResult);
        }

        public async Task<Contact?> GetById(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Contacts
                            .OrderByDescending(p => p.Fullname)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.ContactId == id);

            return item;
        }

        #endregion List

        #region Method

        public async Task<RepositoryResponse> Create(ContactModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Contact item = new Contact()
            {
                Fullname = model.Fullname,
                Address = model.Address,
                Body = model.Body,
                CreateDate = model.CreateDate,
                Email = model.Email,
                Mobile = model.Mobile,
                Subject = model.Subject,
            };

            await _context.Contacts.AddAsync(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.ContactId.ToString(),
            };
        }

        public async Task<RepositoryResponse> Update(string? id, ContactModel model)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var item = await _context.Contacts.FindAsync(id);
            item.Fullname = model.Fullname;
            model.Address = item.Address;
            model.Body = item.Body;
            model.Email = item.Email;
            model.Mobile = item.Mobile;
            model.Subject = item.Subject;
            model.CreateDate = item.CreateDate;

            _context.Contacts.Update(item);

            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id.ToString(),
            };
        }

        public async Task<int> Delete(string? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var item = await _context.Contacts.FindAsync(id);

            _context.Contacts.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion Method
    }
}