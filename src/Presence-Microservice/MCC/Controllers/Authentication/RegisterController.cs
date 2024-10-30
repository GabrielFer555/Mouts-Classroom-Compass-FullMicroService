using MCC.Data.Dtos;
using MCC.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MCC.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto dto)
        {
            if (dto == null)
            {
                return BadRequest("User data is required.");
            }


            try
            {
                await _registerService.RegisterAsync(dto);
                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cadastrar usuário: {ex.Message}");
            }
        }
    }
}
