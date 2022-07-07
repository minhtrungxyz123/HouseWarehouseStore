using HouseWarehouseStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseWarehouse.Store.Api.Controllers
{
    [Route("contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _contactService.GetAll();

            return Ok(comments);
        }
    }
}