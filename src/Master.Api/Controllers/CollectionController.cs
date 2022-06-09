using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("collection")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        #region Fields

        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _collectionService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _collectionService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] CollectionSearchContext ctx)
        {
            var products = await _collectionService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _collectionService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Collection with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CollectionModel model)
        {
            var result = await _collectionService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Collection failed"));
            }
        }

        [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] CollectionModel model, string id)
        {
            var item = await _collectionService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Collection with id: {id} is not found"));

            var result = await _collectionService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Collection failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _collectionService.Delete(id);
            return Ok(result);
        }

        #endregion Method

        #region Utilities

        [Route("get-status-product")]
        [HttpGet]
        public async Task<IActionResult> GetStatusProduct(bool showHidden = true)
        {
            var user = _collectionService.GetStatusProduct(showHidden);
            return Ok(user);
        }

        [Route("get-active")]
        [HttpGet]
        public async Task<IActionResult> GetActive(bool showHidden = true)
        {
            var user = _collectionService.GetActive(showHidden);
            return Ok(user);
        }

        [Route("get-home")]
        [HttpGet]
        public async Task<IActionResult> GetHome(bool showHidden = true)
        {
            var user = _collectionService.GetHome(showHidden);
            return Ok(user);
        }

        [Route("get-hot")]
        [HttpGet]
        public async Task<IActionResult> GetHot(bool showHidden = true)
        {
            var user = _collectionService.GetHot(showHidden);
            return Ok(user);
        }

        #endregion Utilities
    }
}