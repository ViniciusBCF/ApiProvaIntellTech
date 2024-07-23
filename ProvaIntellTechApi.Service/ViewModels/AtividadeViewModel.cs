using ProvaIntellTechApi.Domain.Entities.Enums;
using ProvaIntellTechApi.Service._Helper;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ProvaIntellTechApi.Service.ViewModel
{
    public class AtividadeViewModel
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }

        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [DisplayName("Tipo de Atividade")]
        public TipoDeAtividade TipoAtividade { get; set; }

        [DisplayName("Data da Criação")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataCriacao { get; set; }

        [DisplayName("Data de Atualização")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? DataAtualizacao { get; set; }
    }
}
