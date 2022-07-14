using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class SizeModel
    {
        public string? SizeId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Size")]
        public string SizeProduct { get; set; }
    }
}