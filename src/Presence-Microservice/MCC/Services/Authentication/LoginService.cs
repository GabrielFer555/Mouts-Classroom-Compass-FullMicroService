using AutoMapper;
using MCC.Data.Dtos;
using MCC.Models;
using MCC.Services.Authentication;
using Microsoft.AspNetCore.Identity;

namespace MCC.Services
{
    public class LoginService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public LoginService(
            IMapper mapper,
            UserManager<User> userManager,
            TokenService tokenService,
            SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> LoginAsync(LoginUserDto dto)
        {
            // Tenta fazer login
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!result.Succeeded)
            {
                // Retorna null ou lança uma exceção específica, se necessário
                throw new UnauthorizedAccessException("Usuário ou senha incorretos.");
            }

            // Obtém o usuário após o login bem-sucedido
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            // Gera o token
            var token = _tokenService.GenerateToken(user);
            return await token;
        }
    }
}
