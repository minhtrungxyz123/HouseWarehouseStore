using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("product-like")]
    [ApiController]
    public class ProductLikeController : ControllerBase
    {
        private readonly IProductLikeService _productLikeService;

        public ProductLikeController(IProductLikeService productLikeService)
        {
            _productLikeService = productLikeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string idProd, string idMember)
        {
            var benner = await _productLikeService.GetAll(idMember, idProd);
            return Ok(benner);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ProductLikeSearchContext ctx)
        {
            var products = await _productLikeService.GetAllPaging(ctx);
            return Ok(products);
        }
    }
}