using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseWarehouseStore.Models
{
    public class VoucherModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập thông tin")]
        [Display(Name = "Tên mã giảm giá")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập thông tin"), MaxLength(6, ErrorMessage = "Độ dài Voucher phải là 6 nha !")]
        [Display(Name = "Mã giảm giá")]
        [DataType(DataType.Text)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập thông tin")]
        [Display(Name = "Kiểu giảm giá (%/giá) - Tích là giảm theo %, mặc định là theo giá")]
        public bool Type { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập thông tin")]
        [Display(Name = "Điều kiện giảm giá")]
        public bool Condition { get; set; }

        [Display(Name = "Giảm trên -Nhập 0: là hông giới hạn")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal PriceUp { get; set; }

        [Display(Name = "Giảm dưới-Nhập 0: là hông giới hạn")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal PriceDown { get; set; }

        [Display(Name = "Số lần dùng tối đa")]
        public int SumUse { get; set; }

        [Display(Name = "Giảm tối đa-Nhập 0: là hông giới hạn")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal ReductionMax { get; set; }

        [Display(Name = "Trạng thái kích hoạt")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập thông tin")]
        [Display(Name = "Nhập mức giảm")]
        public decimal Value { get; set; }
    }
}