﻿using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class ProductCategorySearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}