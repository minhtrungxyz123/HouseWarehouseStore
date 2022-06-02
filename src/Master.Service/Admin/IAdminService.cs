using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IAdminService
    {
        Task<IEnumerable<Admin>> GetAll();

        Task<ApiResult<Pagination<Admin>>> GetAllPaging(AdminSearchContext ctx);

        Task<Admin> GetById(int? id);

        Task<RepositoryResponse> Create(AdminModel model);

        Task<RepositoryResponse> Update(int? id, AdminModel model);

        Task<int> Delete(int? id);

        Task<ApiResult<Admin>> GetByIdAsyn(int? id);

        IList<Admin> GetMvcListItems(bool showHidden = true);
    }
}