using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class ProductCategoryModel
    {
        public string? ProductCategorieId { get; set; }

        [Required(ErrorMessage = "Chưa nhập thông tin")]
        [Display(Name = "Tên danh mục")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Ảnh banner")]
        [MaxLength(500)]
        public string? Image { get; set; }

        [Display(Name = "Biểu tượng")]
        [MaxLength(500)]
        public string? CoverImage { get; set; }

        [Display(Name = "Đường dẫn")]
        [MaxLength(500, ErrorMessage = "Tối đa 500 ký tự")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập thông tin")]
        [Display(Name = "Thứ tự")]
        public int Soft { get; set; }

        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }

        [Display(Name = "Hiển trang chủ")]
        public bool Home { get; set; }

        [Display(Name = "Danh mục cha")]
        public string? ParentId { get; set; }

        [Display(Name = "Thẻ tiêu đề")]
        [MaxLength(100)]
        public string TitleMeta { get; set; }

        [Display(Name = "Thẻ mô tả")]
        public string DescriptionMeta { get; set; }

        [Display(Name = "Nội dung sản phẩm")]
        public string Body { get; set; }

        [Display(Name = "Files banner"), Required(ErrorMessage = "Hãy lưu files")]
        public IFormFile? filesadd { get; set; }

        public List<FilesModel>? FilesModels { get; set; }

        [Display(Name = "Files biểu tượng"), Required(ErrorMessage = "Hãy lưu files")]
        public IFormFile? Coverfilesadd { get; set; }

        public List<FilesModel>? CoverFilesModels { get; set; }
    }
}