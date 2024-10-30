using MCC.Data.Dtos;
using MCC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCC.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var token = await _loginService.LoginAsync(dto);
            return Ok(token);
        }
    }
}
