using ProvaIntellTechApi.Domain.Entities.Base;
using System.Linq.Expressions;

namespace ProvaIntellTechApi.Data.Repositories.Interfaces.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> AdicionarAsync(TEntity entity);
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task<List<TEntity>> ObterTodosAsync();
        Task AtualizarAsync(TEntity entity);
        Task RemoverAsync(Guid id);
        Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
