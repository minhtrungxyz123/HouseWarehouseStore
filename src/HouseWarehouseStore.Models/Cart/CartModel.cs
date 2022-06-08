using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseWarehouseStore.Models
{
    public class CartModel
    {
        public string RecordId { get; set; }
        public string CartId { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        public string ProductId { get; set; }

        [Display(Name = "Giá")]
        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        [Display(Name = "Số lượng")]
        public int Count { get; set; }

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
}