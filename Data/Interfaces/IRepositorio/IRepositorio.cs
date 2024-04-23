using System.Linq.Expressions;

namespace Data.Interfaces.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null // Include
        );

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null // Include
        );

        Task Agregar(T entidad);
        void Remover(T entidad);
    }
}