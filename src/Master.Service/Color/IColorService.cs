using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetAll();

        Task<ApiResult<Pagination<Color>>> GetAllPaging(ColorSearchContext ctx);

        Task<Color> GetById(string? id);

        Task<RepositoryResponse> Create(ColorModel model);

        Task<RepositoryResponse> Update(string? id, ColorModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Color>> GetByIdAsyn(string? id);

        IList<Color> GetActive();
    }
}