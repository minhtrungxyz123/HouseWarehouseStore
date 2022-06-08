using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class ColorModel
    {
        public string? ColorId { get; set; }

        [Display(Name = "Mã màu")]
        public string Code { get; set; }

        [Display(Name = "Tên màu")]
        public string NameColor { get; set; }
    }
}