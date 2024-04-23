using Data.Interfaces.IRepositorio;
using Models.Entidades;

namespace Data.Repositorio
{
    public class EspecialidadRepositorio : Repositorio<Especialidad>, IEspecialidadRepositorio
    {
        private readonly ApplicationDbContext _db;

        public EspecialidadRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Especialidad especialidad)
        {
            var especialidadDb = _db.Especialidades.FirstOrDefault(e => e.Id == especialidad.Id);
            if(especialidadDb != null){
                especialidadDb.NombreEspecialidad = especialidad.NombreEspecialidad;
                especialidadDb.Descripcion = especialidad.Descripcion;
                especialidadDb.Estado = especialidad.Estado;
                especialidad.FechaActualizacion = DateTime.Now;
                _db.SaveChanges();
            }
        }
    }
}