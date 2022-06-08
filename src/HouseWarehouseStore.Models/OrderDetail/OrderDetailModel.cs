using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseWarehouseStore.Models
{
    public class OrderDetailModel
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        public string OrderId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
}