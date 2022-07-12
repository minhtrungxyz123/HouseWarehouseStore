using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class MemberModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        [Display(Name = "Tài khoản")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        [Display(Name = "Tên đầy đủ")]
        [MaxLength(50)]
        public string Fullname { get; set; }

        [Display(Name = "Địa chỉ")]
        [MaxLength(200)]
        public string Address { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Mobile { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày tạo")]
        public DateTime CreateDate { get; set; }

        [MaxLength(200)]
        public string HomePage { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        public string Role { get; set; }
        public bool ConfirmEmail { get; set; }
        public string Token { get; set; }
        public bool LockAccount { get; set; }
    }
}