using ProvaIntellTechApi.Data.Context;
using ProvaIntellTechApi.Data.Repositories.Base;
using ProvaIntellTechApi.Data.Repositories.Interfaces;
using ProvaIntellTechApi.Domain.Entities;

namespace ProvaIntellTechApi.Data.Repositories
{
    public class AtividadeRepository : Repository<Atividade>, IAtividadeRepository
    {
        public AtividadeRepository(AppDbContext context) : base(context) { }
    }
}
