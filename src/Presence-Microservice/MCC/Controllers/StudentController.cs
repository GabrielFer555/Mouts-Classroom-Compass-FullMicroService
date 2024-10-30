using MCC.Models;
using MCC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCC.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StudentController : ControllerBase
    {
        private StudentService _service;
        public StudentController(StudentService service)
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

        [Authorize(Roles = "TEACHER,STUDENT")]
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            try
            {
                return StatusCode(201, _service.Create(student));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "TEACHER,STUDENT")]
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

        [Authorize(Roles = "TEACHER,STUDENT")]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Student student)
        {
            try
            {
                _service.Update(student);
                return Ok("Student successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
