
using API.Errores;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entidades;

namespace API.Controllers
{
    // Controladorde prueba de errores posibles
    public class ErrorTestController : BaseApiController
    {
        private readonly ApplicationDbContext _db;
        
        public ErrorTestController(ApplicationDbContext db){
            _db = db;
        }

        // Provocar error, para los que no esten autorizados
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetNotAuthorize(){
            return "No Autorizado";
        }

        // Provocar error, para cuando no se encuentre el recurso
        [HttpGet("not-found")]
        public ActionResult<Usuario> GetNotFound(){
            var objeto = _db.Usuarios.Find(-1);
            if(objeto == null){
                return NotFound(new ApiErrorResponse(404));
            }

            return objeto;
        }

        // Provocar error, para los errores del servidor (convertir nulo en string)
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError(){
            var objeto = _db.Usuarios.Find(-1);
            var objetoString = objeto.ToString();

            return objetoString;
        }

        // Provocar error, para cuando no es valida la solicitud
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest(){
            return BadRequest(new ApiErrorResponse(400));
        }
    }
}