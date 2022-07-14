using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HouseWarehouseStore.Models
{
    public class SizeModel
    {
        public string? SizeId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Size")]
        public string SizeProduct { get; set; }

        [Display(Name = "Sản phẩm")]
        public string? ProductId { get; set; }

        public IList<SelectListItem>? AvailableProduct{ get; set; }
    }
}