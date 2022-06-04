﻿namespace HouseWarehouseStore.Models
{
    public class VoucherModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Type { get; set; }
        public bool Condition { get; set; }
        public decimal PriceUp { get; set; }
        public decimal PriceDown { get; set; }
        public int SumUse { get; set; }
        public decimal ReductionMax { get; set; }
        public bool? Active { get; set; }
        public decimal Value { get; set; }
    }
}