using Models.Entidades;

namespace Data.Interfaces.IRepositorio
{
    public interface IMedicoRepositorio : IRepositorio<Medico>
    {
        void Actualizar(Medico medico);
    }
}