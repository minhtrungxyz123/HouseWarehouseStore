using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAll();

        Task<ApiResult<Pagination<Member>>> GetAllPaging(MemberSearchContext ctx);

        Task<Member> GetById(string? id);

        Task<RepositoryResponse> Create(MemberModel model);

        Task<RepositoryResponse> Update(string? id, MemberModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Member>> GetByIdAsyn(string? id);

        IList<Member> GetMvcListItems(bool showHidden = true);
    }
}