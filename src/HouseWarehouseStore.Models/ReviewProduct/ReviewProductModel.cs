using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class ReviewProductModel
    {
        public string? Id { get; set; }
        public string ProductId { get; set; }
        public int NumberStart { get; set; }

        [Display(Name = "Tên người đánh giá")]
        public string UserId { get; set; }
        public string Content { get; set; }
        public bool StatusReview { get; set; }

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}