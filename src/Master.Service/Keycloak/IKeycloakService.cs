using HouseWarehouseStore.Data.Entities;

namespace Master.Service
{
    public interface IKeycloakService
    {
        Task<Admin> GetById(string? id);
    }
}