using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entidades;

namespace Data.Servicios
{
    public class TokenSercicio : ITokenServicio
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<UsuarioAplicacion> _userManager;

        public TokenSercicio(IConfiguration config, UserManager<UsuarioAplicacion> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _userManager = userManager;
        }

        public async Task<string> CrearToken(UsuarioAplicacion usuario)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };
            var roles = await _userManager.GetRolesAsync(usuario); // Captura el rol al que pertenece el usuario
            // Incluye la infromacion del rol del usuario en el token
            claims.AddRange(roles.Select(rol => new Claim(ClaimTypes.Role, rol)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}