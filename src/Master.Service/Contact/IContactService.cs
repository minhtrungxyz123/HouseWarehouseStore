using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAll();

        Task<ApiResult<Pagination<Contact>>> GetAllPaging(ContactSearchContext ctx);

        Task<Contact> GetById(string? id);

        Task<RepositoryResponse> Create(ContactModel model);

        Task<RepositoryResponse> Update(string? id, ContactModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Contact>> GetByIdAsyn(string? id);
    }
}