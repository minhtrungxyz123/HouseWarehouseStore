using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("product-category")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        #region Fields

        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _productCategoryService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _productCategoryService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ProductCategorySearchContext ctx)
        {
            var products = await _productCategoryService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _productCategoryService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"ProductCategory with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] ProductCategoryModel model)
        {
            var result = await _productCategoryService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create ProductCategory failed"));
            }
        }

        [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] ProductCategoryModel model, string id)
        {
            var item = await _productCategoryService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"ProductCategory with id: {id} is not found"));

            var result = await _productCategoryService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update ProductCategory failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productCategoryService.Delete(id);
            return Ok(result);
        }

        #endregion Method

        #region Utilities

        [Route("get-active")]
        [HttpGet]
        public async Task<IActionResult> GetActive(bool showHidden = true)
        {
            var user = _productCategoryService.GetActive(showHidden);
            return Ok(user);
        }

        [Route("get-home")]
        [HttpGet]
        public async Task<IActionResult> GetHome(bool showHidden = true)
        {
            var user = _productCategoryService.GetHome(showHidden);
            return Ok(user);
        }

        #endregion Utilities
    }
}