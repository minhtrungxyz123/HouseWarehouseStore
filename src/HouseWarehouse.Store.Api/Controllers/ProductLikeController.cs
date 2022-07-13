using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
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

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _productLikeService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"ProductLike with id: {id} is not found"));
            }

            return Ok(item);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] ProductLikeModel model)
        {
            var result = await _productLikeService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create ProductLike failed"));
            }
        }
    }
}