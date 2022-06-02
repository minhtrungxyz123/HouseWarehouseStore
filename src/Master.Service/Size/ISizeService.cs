using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Service
{
    public interface ISizeService
    {
        Task<IEnumerable<Size>> GetAll();

        Task<ApiResult<Pagination<Size>>> GetAllPaging(SizeSearchContext ctx);

        Task<Size> GetById(int? id);

        Task<RepositoryResponse> Create(SizeModel model);

        Task<RepositoryResponse> Update(int? id, SizeModel model);

        Task<int> Delete(int? id);

        Task<ApiResult<Size>> GetByIdAsyn(int? id);
    }
}
