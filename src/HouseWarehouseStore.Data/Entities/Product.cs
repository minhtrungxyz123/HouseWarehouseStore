using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Product
    {
        public string ProductId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string? Body { get; set; }
        public string ProductCategorieId { get; set; }
        public int Quantity { get; set; }
        public string? Factory { get; set; }
        public decimal Price { get; set; }
        public decimal SaleOff { get; set; }
        public string? QuyCach { get; set; }
        public int Sort { get; set; }
        public bool Hot { get; set; }
        public bool Home { get; set; }
        public bool Active { get; set; }
        public string? TitleMeta { get; set; }
        public string? DescriptionMeta { get; set; }
        public string? GiftInfo { get; set; }
        public string? Content { get; set; }
        public bool StatusProduct { get; set; }
        public string CollectionId { get; set; } = null!;
        public string? BarCode { get; set; }
        public DateTime CreateDate { get; set; }
        public string? CreateBy { get; set; }
    }
}
