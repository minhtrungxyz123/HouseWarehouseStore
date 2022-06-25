using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class AdminModel
    {
        public string? Id { get; set; }

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

        [Display(Name = "Quyền")]
        public string Role { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Họ và tên")]
        public string? FullName { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Giới tính")]
        public string? Sex { get; set; }

        [Display(Name = "Tuổi")]
        public string? Age { get; set; }

        [Display(Name = "Chức vụ")]
        public string? Position { get; set; }

        [Display(Name = "Gmail")]
        public  string Email { get; set; }
    }
}