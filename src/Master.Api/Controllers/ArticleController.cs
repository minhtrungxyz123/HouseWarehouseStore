using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        #region Fields

        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _articleService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _articleService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ArticleSearchContext ctx)
        {
            var products = await _articleService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _articleService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Article with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] ArticleModel model)
        {
            var result = await _articleService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Article failed"));
            }
        }

        [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] ArticleModel model, string id)
        {
            var item = await _articleService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Article with id: {id} is not found"));

            var result = await _articleService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update Article failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _articleService.Delete(id);
            return Ok(result);
        }

        #endregion Method

        #region Utilities

        [Route("get-active")]
        [HttpGet]
        public async Task<IActionResult> GetActive(bool showHidden = true)
        {
            var user = _articleService.GetActive(showHidden);
            return Ok(user);
        }

        [Route("get-home")]
        [HttpGet]
        public async Task<IActionResult> GetHome(bool showHidden = true)
        {
            var user = _articleService.GetHome(showHidden);
            return Ok(user);
        }

        [Route("get-hot")]
        [HttpGet]
        public async Task<IActionResult> GetHot(bool showHidden = true)
        {
            var user = _articleService.GetHot(showHidden);
            return Ok(user);
        }

        #endregion Utilities
    }
}