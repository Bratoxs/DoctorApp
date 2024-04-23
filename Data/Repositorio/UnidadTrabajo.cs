using Data.Interfaces.IRepositorio;

namespace Data.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IEspecialidadRepositorio Especialidad { get; private set; }
        public IMedicoRepositorio Medico { get; private set; }
        
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Especialidad = new EspecialidadRepositorio(db);
            Medico = new MedcioRepositorio(db);
        }

        // Para liberar memoria
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}