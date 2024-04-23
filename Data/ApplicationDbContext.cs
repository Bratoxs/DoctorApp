using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<UsuarioAplicacion, RolAplicacion, int, IdentityUserClaim<int>
                                        ,RolUsuarioAplicacion, IdentityUserLogin<int>, IdentityRoleClaim<int>
                                        , IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options){}

        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }

        public DbSet<Medico> Medicos { get; set; }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<HistoriaClinica> HistoriasClinicas { get; set; }

        public DbSet<Antecedente> Antecedentes { get; set; }

        // Por cada entidad que se cree, se requiere un archivo de configuracion, se crea carpeta Configuraciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}