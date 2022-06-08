using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class UserVoucherModel
    {
        public string? Id { get; set; }
        public string MaDonHang { get; set; }
        public decimal SumHd { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập thông tin"), MaxLength(6, ErrorMessage = "Độ dài Voucher phải là 6 nha !")]
        [Display(Name = "Mã giảm giá")]
        [DataType(DataType.Text)]
        public string Code { get; set; }
    }
}