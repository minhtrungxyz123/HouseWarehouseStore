using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseWarehouseStore.Data.Entities
{
    public partial class OrderDetail
    {
        [Key, Column(Order = 1)]
        public string ProductId { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        [Key, Column(Order = 0)]
        public string OrderId { get; set; }

        [Key, Column(Order = 3)]
        public string Color { get; set; }

        [Key, Column(Order = 2)]
        public string Size { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}