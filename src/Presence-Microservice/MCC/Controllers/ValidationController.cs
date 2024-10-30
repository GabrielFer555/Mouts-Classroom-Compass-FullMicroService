using MCC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCC.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ValidationController : ControllerBase
    {
        private readonly ValidationService _service;

        public ValidationController(ValidationService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(Guid subject, Guid teacher, Guid student)
        {
            try
            {
                var result = _service.ValidateData(subject, teacher, student);

                if (result == null)
                    return BadRequest("Some of the IDs do not exist or the teacher is not linked to the subject");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
