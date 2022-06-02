using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAll();

        Task<ApiResult<Pagination<Contact>>> GetAllPaging(ContactSearchContext ctx);

        Task<Contact> GetById(int? id);

        Task<RepositoryResponse> Create(ContactModel model);

        Task<RepositoryResponse> Update(int? id, ContactModel model);

        Task<int> Delete(int? id);

        Task<ApiResult<Contact>> GetByIdAsyn(int? id);
    }
}