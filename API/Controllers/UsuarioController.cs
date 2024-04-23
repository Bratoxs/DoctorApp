using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using Models.DTOs;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace API.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly ITokenServicio _tokenService;
        private ApiResponse _response;
        private readonly RoleManager<RolAplicacion> _rolManager;

        public UsuarioController(UserManager<UsuarioAplicacion> userManager, ITokenServicio tokenService,
                                RoleManager<RolAplicacion> rolManager){
            _userManager = userManager;
            _tokenService = tokenService;
            _response = new();
            _rolManager = rolManager;
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet]
        public async Task<ActionResult> GetUsuarios(){
            var usuarios = await _userManager.Users.Select(u => new UsuarioListaDto()
            {
                Username = u.UserName,
                Apellidos = u.Apellidos,
                Nombres = u.Nombres,
                Email = u.Email,
                Rol = string.Join(",", _userManager.GetRolesAsync(u).Result.ToArray())
            }).ToListAsync();
            _response.Resultado = usuarios;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        /*
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id){
            var usuario = await _db.Usuarios.FindAsync(id);
            return Ok(usuario);
        }
        */

        [Authorize(Policy = "AdminRol")]
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto){
            if (await UsuarioExiste(registroDto.Username)){
                return BadRequest("Username ya esta registrado");
            }
            var usuario = new UsuarioAplicacion{
                UserName = registroDto.Username.ToLower(),
                Email = registroDto.Email,
                Apellidos = registroDto.Apellidos,
                Nombres = registroDto.Nombres
            };
            var resultado = await _userManager.CreateAsync(usuario, registroDto.Password);

            if(!resultado.Succeeded){ // Validar que el usuario se creo sin problemas
                return BadRequest(resultado.Errors);
            }
            
            // Agregar rol al usuario que se creo
            var rolResultado = await _userManager.AddToRoleAsync(usuario, registroDto.Rol);
            if(!rolResultado.Succeeded){
                return BadRequest("Error al Agregar el Rol al Usuario");
            }

            return new UsuarioDto{
                Username = usuario.UserName,
                Token = await _tokenService.CrearToken(usuario)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto){
            var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if(usuario == null){
                return Unauthorized("Usuario no válido");
            }

            var resultado = await _userManager.CheckPasswordAsync(usuario, loginDto.Password);

            if(!resultado){
                return Unauthorized("Password no valido");
            }

            return new UsuarioDto{
                Username = usuario.UserName,
                Token = await _tokenService.CrearToken(usuario)
            };
        }

        [Authorize(Policy = "AdminRol")]
        [HttpGet("ListadoRoles")]
        public IActionResult GetRoles(){
            var roles = _rolManager.Roles.Select(r => new { NombreRol = r.Name }).ToList();
            _response.Resultado = roles;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        // Método para validar si el username ya se encuentra creado
        private async Task<bool> UsuarioExiste(string username){
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}