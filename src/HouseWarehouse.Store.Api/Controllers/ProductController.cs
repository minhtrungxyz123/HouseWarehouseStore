using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(bool showHidden = true)
        {
            var comments = await _productService.GetAll(showHidden);

            return Ok(comments);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ProductSearchContext ctx)
        {
            var products = await _productService.GetAllPaging(ctx);
            return Ok(products);
        }
    }
}