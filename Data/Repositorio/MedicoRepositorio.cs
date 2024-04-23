using Data.Interfaces.IRepositorio;
using Models.Entidades;

namespace Data.Repositorio
{
    public class MedcioRepositorio : Repositorio<Medico>, IMedicoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MedcioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Medico medico)
        {
            var medicoDb = _db.Medicos.FirstOrDefault(e => e.Id == medico.Id);
            if(medicoDb != null){
                medicoDb.Apellidos = medico.Apellidos;
                medicoDb.Nombres = medico.Nombres;
                medicoDb.Estado = medico.Estado;
                medicoDb.FechaActualizacion = DateTime.Now;
                medicoDb.Telefono = medico.Telefono;
                medicoDb.Genero = medico.Genero;
                medicoDb.EspecialidadId = medico.EspecialidadId;
                medicoDb.Direccion = medico.Direccion;
                _db.SaveChanges();
            }
        }
    }
}