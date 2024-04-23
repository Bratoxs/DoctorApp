using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace Data.Inicializador
{
    public class DbInicializardor : IdbInicializador
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly RoleManager<RolAplicacion> _rolManager;

        public DbInicializardor(ApplicationDbContext db, UserManager<UsuarioAplicacion> userManager, RoleManager<RolAplicacion> rolManager)
        {
            _db = db;
            _userManager = userManager;
            _rolManager = rolManager;
        }

        public void Inicializar()
        {
            try
            {
                // Validar cuando se ejecuta por primera vez hay migraciones pendientes ejecutarlas
                if(_db.Database.GetPendingMigrations().Count() > 0){
                    _db.Database.Migrate();
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            // Datos Iniciales
            // Craer Roles
            if(_db.Roles.Any(r => r.Name == "Admin")){ // Verifico si el rol Admin existe, si existe retorno
                return;
            }

            // Si no existe creados, creo los roles
            _rolManager.CreateAsync(new RolAplicacion { Name = "Admin" }).GetAwaiter().GetResult();
            _rolManager.CreateAsync(new RolAplicacion { Name = "Agendador" }).GetAwaiter().GetResult();
            _rolManager.CreateAsync(new RolAplicacion { Name = "Doctor" }).GetAwaiter().GetResult();

            // Crear usuario administrador
            var usuario = new UsuarioAplicacion{
                UserName = "administrador",
                Email = "administrador@doctorapp.com",
                Apellidos = "Bravo",
                Nombres = "Marco"
            };

            _userManager.CreateAsync(usuario, "Admin123").GetAwaiter().GetResult();

            // Asignar el Rol Admin al usuario
            UsuarioAplicacion usuarioAdmin = _db.UsuarioAplicacion.Where(u => u.UserName == "administrador").FirstOrDefault();
            _userManager.AddToRoleAsync(usuarioAdmin, "Admin").GetAwaiter().GetResult();
        }
    }
}