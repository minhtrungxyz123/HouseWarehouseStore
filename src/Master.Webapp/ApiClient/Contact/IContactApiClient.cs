using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IContactApiClient
    {
        public Task<bool> Create(ContactModel request);

        public Task<bool> Edit(int? id, ContactModel request);

        Task<ApiResult<Pagination<ContactModel>>> Get(ContactSearchModel request);

        Task<ApiResult<ContactModel>> GetById(int id);

        Task<bool> Delete(int id);
    }
}
