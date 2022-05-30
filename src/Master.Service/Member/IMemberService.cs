using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAll();

        Task<ApiResult<Pagination<Member>>> GetAllPaging(MemberSearchModel ctx);

        Task<Member> GetById(int? id);

        Task<RepositoryResponse> Create(MemberModel model);

        Task<RepositoryResponse> Update(int? id, MemberModel model);

        Task<int> Delete(int? id);

        Task<ApiResult<Member>> GetByIdAsyn(int? id);

        IList<Member> GetMvcListItems(bool showHidden = true);
    }
}