using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("follow")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        #region Fields

        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _followService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] FollowSearchContext ctx)
        {
            var products = await _followService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _followService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Follow with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] FollowModel model)
        {
            var result = await _followService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Follow failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] FollowModel model, string id)
        {
            var item = await _followService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Follow with id: {id} is not found"));

            var result = await _followService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Follow failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _followService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}