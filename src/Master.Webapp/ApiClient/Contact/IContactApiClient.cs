using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IContactApiClient
    {
        public Task<bool> Create(ContactModel request);

        public Task<bool> Edit(string? id, ContactModel request);

        Task<ApiResult<Pagination<ContactModel>>> Get(ContactSearchModel request);

        Task<ApiResult<ContactModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}
