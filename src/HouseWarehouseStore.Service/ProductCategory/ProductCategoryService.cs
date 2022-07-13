using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseWarehouseStore.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly HouseWarehouseStoreDbContext _context;

        public ProductCategoryService(HouseWarehouseStoreDbContext context)
        {
            _context = context;
        }

        public Task<ApiResult<Pagination<ProductCategoryModel>>> GetAllPaging(ProductCategorySearchContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}
