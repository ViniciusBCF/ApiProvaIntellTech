using ProvaIntellTechApi.Domain.Entities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProvaIntellTechApi.Service.DTOs
{
    public class AtividadeDto
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Nome { get; set; }

        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [DisplayName("Tipo de Atividade")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoDeAtividade TipoAtividade { get; set; }

        [DisplayName("Data da Criação")]
        [JsonIgnore]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DateTime DataCriacao { get; set; }

        [DisplayName("Data de Atualização")]
        [JsonIgnore]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DateTime? DataAtualizacao { get; set; }
    }
}
