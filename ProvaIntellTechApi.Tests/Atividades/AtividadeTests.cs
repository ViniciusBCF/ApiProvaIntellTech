using AutoFixture;
using Bogus;
using FluentAssertions;
using ProvaIntellTechApi.Domain.Entities;
using ProvaIntellTechApi.Domain.Entities.Enums;

namespace ProvaIntellTechApi.Tests
{
    [Trait("Atividade", nameof(Atividade))]
    public class AtividadeTests
    {
        private readonly Faker _faker = new Faker();
        private readonly Fixture _fixtures = new Fixture();

        [Fact]
        public void AlterarNome_DeveAlterar()
        {
            var atividade = _fixtures.Create<Atividade>();
            var nome = _faker.Name.FullName();

            atividade.AlterarNome(nome);

            atividade.Nome.Should().Be(nome);
        }

        [Fact]
        public void AlterarDescricao_DeveAlterar()
        {
            var atividade = _fixtures.Create<Atividade>();
            var descricao = _faker.Lorem.Text();

            atividade.AlterarDescricao(descricao);

            atividade.Descricao.Should().Be(descricao);
        }

        [Fact]
        public void AlterarTipoAtividade_DeveAlterar()
        {
            var atividade = _fixtures.Create<Atividade>();
            var tipoDeAtividade = _faker.PickRandom<TipoDeAtividade>();

            atividade.AlterarTipoAtividade(tipoDeAtividade);

            atividade.TipoAtividade.Should().Be(tipoDeAtividade);
        }
    }
}
