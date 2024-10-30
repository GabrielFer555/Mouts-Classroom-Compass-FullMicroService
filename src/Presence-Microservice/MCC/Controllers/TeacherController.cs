using MCC.Models;
using MCC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCC.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TeacherController : ControllerBase
    {
        private TeacherService _service;
        public TeacherController(TeacherService service)
        {
            _service = service;
        }


        [Authorize(Roles = "TEACHER,STUDENT")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "TEACHER,STUDENT")]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(_service.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "TEACHER")]
        [HttpPost]
        public IActionResult Post([FromBody] Teacher teacher)
        {
            try
            {
                return StatusCode(201, _service.Create(teacher));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "TEACHER")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "TEACHER")]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Teacher teacher)
        {
            try
            {
                _service.Update(teacher);
                return Ok("Teacher successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
