using Models.Entidades;

namespace Data.Interfaces.IRepositorio
{
    public interface IEspecialidadRepositorio : IRepositorio<Especialidad>
    {
        void Actualizar(Especialidad especialidad);
    }
}