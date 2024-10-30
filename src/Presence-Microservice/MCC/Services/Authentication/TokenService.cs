using MCC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MCC.Services.Authentication
{
    public class TokenService
    {
        private readonly UserManager<User> _userManager;

        public TokenService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GenerateToken(User user)
        {
            // Obtém as roles do usuário
            var roles = await _userManager.GetRolesAsync(user);

            // Cria a lista de claims com email e id
            var claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id)
            };

            // Adiciona cada role como uma claim
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Configuração da chave de segurança
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1a28bedb5a055526375fdc044d3133ffa318e9432b4e2e2b890135058e7a032a515aede2c28bae62e7946736b72fbd36a40c678b6a00b28e9c77ca547f4d657d3465115226947adc960815f168e48fb450646bca8c771cdea1538ba17449c4e438fe77fd77ffaabeeafc107aec8cc978d9a9a14be6f148b1e5d5a8fe4700d551171e811901a89221ac766c91b368a3397c742d2b901141ce44e30c9fcfba5f1777f54cfeff33ccf25ba34f5f0c35302985e1b72bd9c5bcce7cc369ad320d66837561dfbfc4035966b91edb4d00cdf16a037ef974118520c565a4a49c6f084f3f3c6fece3f1844a554de8a68d5505e1b29f2b3545597c1eabd99ff1824a221d8e"));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Criação do token
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(30),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
