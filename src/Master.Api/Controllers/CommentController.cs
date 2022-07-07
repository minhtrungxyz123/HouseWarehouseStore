using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        #region Fields

        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        #endregion Fields

        #region List

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] CommentSearchContext ctx)
        {
            var products = await _commentService.GetAllPaging(ctx);
            return Ok(products);
        }

        #endregion List

        #region Method

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _commentService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}