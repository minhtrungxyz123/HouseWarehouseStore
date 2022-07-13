using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface IProductLikeService
    {
        Task<List<ProductLikeModel>> GetAll(string idProd, string idMember);

        Task<ApiResult<Pagination<ProductLikeModel>>> GetAllPaging(ProductLikeSearchContext ctx);

        Task<ProductLike> GetById(string id);

        Task<RepositoryResponse> Create(ProductLikeModel model);
    }
}