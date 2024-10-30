using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCC.Controllers.Authentication
{
    [ApiController]
    [Route("[Controller]")]
    public class AcessController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "Role")]
        public IActionResult Get()
        {
            return Ok("Acesse permirito");
        }
    }
}
