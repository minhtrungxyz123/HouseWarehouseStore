using System;
using System.Collections.Generic;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class Voucher
    {
        public string? Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public bool Type { get; set; }
        public bool Condition { get; set; }
        public decimal PriceUp { get; set; }
        public decimal PriceDown { get; set; }
        public int SumUse { get; set; }
        public decimal ReductionMax { get; set; }
        public bool Active { get; set; }
        public decimal Value { get; set; }
    }
}
