using HouseWarehouseStore.Common;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("product-like")]
    [ApiController]
    public class ProductLikeController : ControllerBase
    {
        #region Fields

        private readonly IProductLikeService _productLikeService;

        public ProductLikeController(IProductLikeService productLikeService)
        {
            _productLikeService = productLikeService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _productLikeService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _productLikeService.GetAll());
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

        #endregion List

        #region Method

        #endregion Method

        #region Utilities

        #endregion Utilities
    }
}
