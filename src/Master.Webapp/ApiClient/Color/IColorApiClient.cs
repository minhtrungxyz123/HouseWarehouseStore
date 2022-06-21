using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IColorApiClient
    {
        public Task<bool> Create(ColorModel request);

        public Task<bool> Edit(string? id, ColorModel request);

        Task<ApiResult<Pagination<ColorModel>>> Get(ColorSearchModel request);

        Task<ApiResult<ColorModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<IList<ColorModel>> GetActive();
    }
}
