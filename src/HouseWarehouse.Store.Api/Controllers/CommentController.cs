using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentService.GetAll();

            return Ok(comments);
        }
    }
}