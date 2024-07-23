using Microsoft.EntityFrameworkCore;
using ProvaIntellTechApi.Data.Context;
using ProvaIntellTechApi.Data.Repositories.Interfaces.Base;
using ProvaIntellTechApi.Domain.Entities.Base;
using System.Linq.Expressions;

namespace ProvaIntellTechApi.Data.Repositories.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodosAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> AdicionarAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task AtualizarAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();
        }

        public virtual async Task RemoverAsync(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
