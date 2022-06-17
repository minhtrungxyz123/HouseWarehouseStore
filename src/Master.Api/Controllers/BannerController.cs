using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("banner")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        #region Fields

        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _bannerService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _bannerService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] BannerSearchContext ctx)
        {
            var products = await _bannerService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _bannerService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Banner with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] BannerModel model)
        {
            var result = await _bannerService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Banner failed"));
            }
        }

        [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] BannerModel model, string id)
        {
            var item = await _bannerService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Banner with id: {id} is not found"));

            var result = await _bannerService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Banner failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _bannerService.Delete(id);
            return Ok(result);
        }

        #endregion Method

        #region Utilities

        [Route("get-active")]
        [HttpGet]
        public async Task<IActionResult> GetActive(bool showHidden = true)
        {
            var user = _bannerService.GetActive(showHidden);
            return Ok(user);
        }

        #endregion Utilities
    }
}