using HouseWarehouseStore.Common;
using Master.Service;
using Microsoft.AspNetCore.Mvc;

namespace Master.Api.Controllers
{
    [Route("keycloak")]
    [ApiController]
    public class KeycloakController : Controller
    {
        #region Fields

        private readonly IKeycloakService _keycloakService;

        public KeycloakController(IKeycloakService keycloakService)
        {
            _keycloakService = keycloakService;
        }

        #endregion Fields

        #region List

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var item = await _keycloakService.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Keycloak with id: {id} is not found"));
            }

            return Ok(item);
        }

        #endregion List
    }
}