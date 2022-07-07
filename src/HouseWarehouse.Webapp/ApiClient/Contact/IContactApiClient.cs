using HouseWarehouseStore.Models;

namespace HouseWarehouse.Webapp.ApiClient
{
    public interface IContactApiClient
    {
        Task<List<ContactModel>> GetAll();
    }
}