using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("Follow")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _FollowService;

        public FollowController(IFollowService FollowService)
        {
            _FollowService = FollowService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _FollowService.GetAll();

            return Ok(comments);
        }
    }
}