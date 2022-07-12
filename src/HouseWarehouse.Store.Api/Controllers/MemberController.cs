using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;
using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        #region Fields

        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        #endregion Fields

        #region Method

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _memberService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Member with id: {id} is not found"));
            }

            return Ok(item);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] MemberModel model)
        {
            var result = await _memberService.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create Member failed"));
            }
        }

        [Route("check-active")]
        [HttpGet]
        public async Task<IActionResult> GetCheckActive(string name, bool showHidden = true)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            var checkActive = await _memberService.GetCheckActive(name, showHidden);

            if (checkActive == null)
            {
                return NotFound(new ApiNotFoundResponse($"member with name: {name} is not found"));
            }

            return Ok(checkActive);
        }

        #endregion Method
    }
}