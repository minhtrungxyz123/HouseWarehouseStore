﻿namespace HouseWarehouseStore.Data.Entities
{
    public partial class OrderDetail
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}