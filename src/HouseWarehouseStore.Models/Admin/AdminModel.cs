using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class AdminModel
    {
        public string? AdminId { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        [Display(Name = "Tài khoản")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        [Display(Name = "Trạng thái")]
        public bool Active { get; set; }

        public string Role { get; set; }
    }
}