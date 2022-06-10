using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("article-category")]
    [ApiController]
    public class ArticleCategoriesController : ControllerBase
    {
        #region Fields

        private readonly IArticleCategoryService _articleCategoryService;

        public ArticleCategoriesController(IArticleCategoryService articleCategoryService)
        {
            _articleCategoryService = articleCategoryService;
        }

        #endregion Fields

        #region List

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _articleCategoryService.GetByIdAsyn(id);
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _articleCategoryService.GetAll());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ArticleCategorySearchContext ctx)
        {
            var products = await _articleCategoryService.GetAllPaging(ctx);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _articleCategoryService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"ArticleCategory with id: {id} is not found"));
            }

            return Ok(item);
        }

        [Route("get-category-active")]
        [HttpGet]
        public async Task<IActionResult> GetCategoryActive(bool showHidden = true)
        {
            var user = _articleCategoryService.GetCategoryActive(showHidden);
            return Ok(user);
        }

        [Route("get-hot")]
        [HttpGet]
        public async Task<IActionResult> GetHot(bool showHidden = true)
        {
            var user = _articleCategoryService.GetHot(showHidden);
            return Ok(user);
        }

        [Route("get-showhome")]
        [HttpGet]
        public async Task<IActionResult> GetShowHome(bool showHidden = true)
        {
            var user = _articleCategoryService.GetShowHome(showHidden);
            return Ok(user);
        }

        [Route("get-showmenu")]
        [HttpGet]
        public async Task<IActionResult> GetShowMenu(bool showHidden = true)
        {
            var user = _articleCategoryService.GetShowMenu(showHidden);
            return Ok(user);
        }

        #endregion List

        #region Method

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] ArticleCategoryModel model)
        {
            var result = await _articleCategoryService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create ArticleCategory failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromBody] ArticleCategoryModel model, string id)
        {
            var item = await _articleCategoryService.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"ArticleCategory with id: {id} is not found"));

            var result = await _articleCategoryService.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update ArticleCategory failed"));
            }
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _articleCategoryService.Delete(id);
            return Ok(result);
        }

        #endregion Method
    }
}