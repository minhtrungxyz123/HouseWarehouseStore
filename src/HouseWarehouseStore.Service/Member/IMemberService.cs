using HouseWarehouseStore.Data.Entities;

namespace HouseWarehouseStore.Service
{
    public interface IMemberService
    {
        Task<Member> GetCheckActive(string name, bool showHidden = true);
    }
}