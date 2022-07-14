using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("product-size-color")]
    [ApiController]
    public class ProductSizeColorController : ControllerBase
    {
        private readonly IProductSizeColorService _productSizeColorService;

        public ProductSizeColorController(IProductSizeColorService productSizeColorService)
        {
            _productSizeColorService = productSizeColorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string id)
        {
            var benner = await _productSizeColorService.GetAll(id);
            return Ok(benner);
        }
    }
}