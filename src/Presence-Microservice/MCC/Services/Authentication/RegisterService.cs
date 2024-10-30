using AutoMapper;
using MCC.Data.Dtos;
using MCC.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MCC.Services.Authentication
{
    public class RegisterService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; // Adicionando RoleManager

        public RegisterService(IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager; // Inicializando RoleManager
        }

        public async Task RegisterAsync(CreateUserDto dto)
        {
            // Map the DTO to the User entity
            User user = _mapper.Map<User>(dto);
            user.UserName = dto.Email; // ou `dto.UserName` se você tiver esse campo

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Falha ao cadastrar: {errors}");
            }


            // Check if Role is valid before adding
            if (!string.IsNullOrEmpty(dto.Role))
            {
                // Verificar se a role existe usando RoleManager
                var roleExists = await _roleManager.RoleExistsAsync(dto.Role);
                if (roleExists)
                {
                    await _userManager.AddToRoleAsync(user, dto.Role);
                }
                else
                {
                    throw new Exception("Role inválida.");
                }
            }
            else
            {
                throw new Exception("Role não pode ser nula ou vazia.");
            }


        }
    }


}
