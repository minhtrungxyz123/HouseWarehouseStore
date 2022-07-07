using HouseWarehouseStore.Data.Entities;

namespace HouseWarehouseStore.Service
{
    public interface IContactService
    {
        Task<List<Contact>> GetAll();
    }
}