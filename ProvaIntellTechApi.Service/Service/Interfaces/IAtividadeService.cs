using ProvaIntellTechApi.Service.Results;
using ProvaIntellTechApi.Service.ViewModel;

namespace ProvaIntellTechApi.Service.Service.Interfaces
{
    public interface IAtividadeService : IDisposable
    {
        Task<Result<IEnumerable<AtividadeViewModel>>> ObterTodosAsync();
        Task<Result<AtividadeViewModel>> ObterPorIdAsync(Guid id);
        Task<Result<IEnumerable<object>>> ObterTiposAtividade();
        Task<Result<AtividadeViewModel>> AdicionarAsync(AtividadeViewModel atividade);
        Task<Result<AtividadeViewModel>> AtualizarAsync(Guid id, AtividadeViewModel atividade);
        Task<Result<AtividadeViewModel>> RemoverAsync(Guid id);
    }
}
