using ProvaIntellTechApi.Domain.Entities.Base;
using ProvaIntellTechApi.Domain.Entities.Enums;

namespace ProvaIntellTechApi.Domain.Entities
{
    public class Atividade : Entity
    {
        public TipoDeAtividade TipoAtividade { get; private set; }
        public string? Nome { get; private set; }
        public string? Descricao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }
        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }
        public void AlterarTipoAtividade(TipoDeAtividade tipoDeAtividade)
        {
            TipoAtividade = tipoDeAtividade;
        }
    }
}
