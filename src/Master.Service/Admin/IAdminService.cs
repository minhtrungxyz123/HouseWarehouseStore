using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IAdminService
    {
        Task<IEnumerable<Admin>> GetAll();

        Task<ApiResult<Pagination<Admin>>> GetAllPaging(AdminSearchContext ctx);

        Task<Admin> GetById(string? id);

        Task<RepositoryResponse> Create(AdminModel model);

        Task<RepositoryResponse> Update(string? id, AdminModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Admin>> GetByIdAsyn(string? id);

        IList<Admin> GetMvcListItems(bool showHidden = true);
    }
}