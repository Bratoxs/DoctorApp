
namespace Data.Interfaces.IRepositorio
{
    // IDisposable: Provee un mecanismo para liberar objetos de la memoria que no esten siendo administrados por el sistema
    public interface IUnidadTrabajo : IDisposable
    {
        IEspecialidadRepositorio Especialidad { get; }
        IMedicoRepositorio Medico { get; }

        Task Guardar();
    }
}