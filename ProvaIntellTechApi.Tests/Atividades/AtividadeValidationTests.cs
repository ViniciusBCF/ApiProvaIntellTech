using AutoFixture;
using Bogus;
using FluentAssertions;
using ProvaIntellTechApi.Domain._Helper;
using ProvaIntellTechApi.Service.Notifications;
using ProvaIntellTechApi.Service.Notifications.Interfaces;
using ProvaIntellTechApi.Service.Validation;
using ProvaIntellTechApi.Service.ViewModel;
using ProvaIntellTechApi.Tests._Helper;

namespace ProvaIntellTechApi.Tests.Atividades
{
    [Trait("Atividade", nameof(AtividadeValidation))]
    public class AtividadeValidationTests
    {
        private readonly Faker _faker;
        private readonly Fixture _fixtures;
        private readonly INotificador _notificador;
        private readonly ValidationHelper _validation;

        public AtividadeValidationTests()
        {
            _faker = new Faker();
            _fixtures = new Fixture();
            _notificador = new Notificador();
            _validation = new ValidationHelper(_notificador);
        }


        [Fact]
        public void ExecutarValidacao_TodosValoresValidos_RetornaTrue()
        {

            var atividade = _fixtures.Create<AtividadeViewModel>();

            var validacaoAtividade = _validation.ExecutarValidacao(new AtividadeValidation(), atividade);

            validacaoAtividade.Should().BeTrue();
            _notificador.ObterNotificacoes().Should().BeEmpty();
        }

        [Fact]
        public void ExecutarValidacao_NomeVazio_RetornaFalseComNotificacao()
        {
            var mensagemEsperada = Constantes.CampoVazioErrorMsg(nameof(AtividadeViewModel.Nome));
            var atividade = _fixtures.Build<AtividadeViewModel>()
                                        .With(a => a.Nome, string.Empty)
                                        .Create();

            var validacaoAtividade = _validation.ExecutarValidacao(new AtividadeValidation(), atividade);

            validacaoAtividade.Should().BeFalse();
            _notificador.TemNotificacao().Should().BeTrue();
            _notificador.ObtemNotificacao(mensagemEsperada).Should().BeTrue();
        }

        [Fact]
        public void ExecutarValidacao_NomeMaiorQueEsperado_RetornaFalseComNotificacao()
        {
            var mensagemEsperada = Constantes.CampoMaiorErrorMsg(nameof(AtividadeViewModel.Nome), Constantes.Numero128);
            var nomeMaiorPermitido = _faker.Random.String2(Constantes.Numero129);
            var atividade = _fixtures.Build<AtividadeViewModel>()
                                        .With(a => a.Nome, nomeMaiorPermitido)
                                        .Create();

            var validacaoAtividade = _validation.ExecutarValidacao(new AtividadeValidation(), atividade);

            validacaoAtividade.Should().BeFalse();
            _notificador.ObtemNotificacao(mensagemEsperada).Should().BeTrue();
        }

        [Fact]
        public void ExecutarValidacao_DescricaoVazia_RetornaFalseComNotificacao()
        {
            var mensagemEsperada = Constantes.CampoVazioErrorMsg(nameof(AtividadeViewModel.Descricao));
            var atividade = _fixtures.Build<AtividadeViewModel>()
                                        .With(a => a.Descricao, string.Empty)
                                        .Create();

            var validacaoAtividade = _validation.ExecutarValidacao(new AtividadeValidation(), atividade);

            validacaoAtividade.Should().BeFalse();
            _notificador.ObtemNotificacao(mensagemEsperada).Should().BeTrue();
        }

        [Fact]
        public void ExecutarValidacao_DescricaoMaiorQueEsperado_RetornaFalseComNotificacao()
        {
            var mensagemEsperada = Constantes.CampoMaiorErrorMsg(nameof(AtividadeViewModel.Descricao), Constantes.Numero512);
            var descricaoMaiorPermitido = _faker.Random.String2(Constantes.Numero513);
            var atividade = _fixtures.Build<AtividadeViewModel>()
                                        .With(a => a.Descricao, descricaoMaiorPermitido)
                                        .Create();

            var validacaoAtividade = _validation.ExecutarValidacao(new AtividadeValidation(), atividade);

            validacaoAtividade.Should().BeFalse();
            _notificador.ObtemNotificacao(mensagemEsperada).Should().BeTrue();
        }
    }
}
