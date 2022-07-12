using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface IMemberService
    {
        Task<Member> GetCheckActive(string name, bool showHidden = true);

        Task<Member> GetById(string id);

        Task<RepositoryResponse> Create(MemberModel model);
    }
}