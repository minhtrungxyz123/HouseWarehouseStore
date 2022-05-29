﻿namespace HouseWarehouseStore.Models
{
    public class OrderDetailModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
}