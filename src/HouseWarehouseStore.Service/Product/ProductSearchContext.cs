﻿namespace HouseWarehouseStore.Service
{
    public class ProductSearchContext
    {
        public string? Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public decimal? Price { get; set; }
    }
}