﻿using HouseWarehouseStore.Common;

namespace HouseWarehouseStore.Models
{
    public class ProductLikeSearchModel : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public  string? IdMember { get; set; }
    }
}