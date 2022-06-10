using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class ArticleModel
    {
        public string? Id { get; set; }

        [Display(Name = "Tiêu đề", Description = "Tiêu đề dài tối đa 100 ký tự"), Required(ErrorMessage = "Hãy nhập tiêu đề"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string Subject { get; set; }

        [Display(Name = "Trích dẫn ngắn"), Required(ErrorMessage = "Hãy nhập trích dẫn ngắn"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Nội dung"), UIHint("EditorBox")]
        public string Body { get; set; }

        [Display(Name = "Hình ảnh đại diện"), UIHint("ImageArticle")]
        public string Image { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Display(Name = "Ngày đăng")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Lượt xem")]
        public int View { get; set; }

        [Display(Name = "Danh mục bài viết"), Required(ErrorMessage = "Hãy chọn danh mục bài viết")]
        public string ArticleCategoryId { get; set; }

        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }

        [Display(Name = "Tin nổi bật")]
        public bool Hot { get; set; }

        [Display(Name = "Hiện trang chủ")]
        public bool Home { get; set; }
        public string Url { get; set; }

        [Display(Name = "Thẻ tiêu đề"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string TitleMeta { get; set; }

        [Display(Name = "Từ khóa"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string KeyWord { get; set; }

        [Display(Name = "Thẻ mô tả"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string DescriptionMetaTitle { get; set; }

        public IFormFile? filesadd { get; set; }

        public List<FilesModel>? FilesModels { get; set; }
    }
}