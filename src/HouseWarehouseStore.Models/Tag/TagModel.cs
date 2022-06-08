using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class TagModel
    {
        public string? TagId { get; set; }

        [StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), Display(Name = "Tên nhóm sản phẩm"), Required(ErrorMessage = "Hãy nhập nhóm sản phẩm"), UIHint("TextBox")]
        public string Name { get; set; }

        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Soft { get; set; }

        [Required]
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
    }
}