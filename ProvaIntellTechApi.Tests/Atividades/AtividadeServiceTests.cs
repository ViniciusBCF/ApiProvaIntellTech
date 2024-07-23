﻿using AutoFixture;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using ProvaIntellTechApi.Configuration;
using ProvaIntellTechApi.Data.Repositories.Interfaces;
using ProvaIntellTechApi.Domain._Helper;
using ProvaIntellTechApi.Domain.Entities;
using ProvaIntellTechApi.Domain.Entities.Enums;
using ProvaIntellTechApi.Service.Notifications;
using ProvaIntellTechApi.Service.Notifications.Interfaces;
using ProvaIntellTechApi.Service.Results.Enums;
using ProvaIntellTechApi.Service.Service;
using ProvaIntellTechApi.Service.Service.Interfaces;
using ProvaIntellTechApi.Service.ViewModel;
using ProvaIntellTechApi.Tests._Helper;

namespace ProvaIntellTechApi.Tests.Atividades
{
    [Trait("Atividade", nameof(AtividadeService))]
    public class AtividadeServiceTests
    {
        private readonly Faker _faker;
        private readonly Fixture _fixtures;
        private readonly IAtividadeService _atividadeService;
        private readonly Mock<IAtividadeRepository> _atividadeRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;
        public AtividadeServiceTests()
        {
            _faker = new Faker();
            _fixtures = new Fixture();
            _atividadeRepository = new Mock<IAtividadeRepository>();
            _notificador = new Notificador();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });
            _mapper = mappingConfig.CreateMapper();
            _atividadeService = new AtividadeService
            (
                _atividadeRepository.Object,
                _notificador,
                _mapper
            );
        }

        [Fact]
        public async Task AdicionarAsync_QuandoEntidadeValida_DeveRetornarOk()
        {
            var atividade = _fixtures.Create<AtividadeViewModel>();

            var result = await _atividadeService.AdicionarAsync(atividade);

            result.IsValid.Should().BeTrue();
            result.StatusCode.Should().Be(StatusCodeResultEnum.Ok);
        }

        [Fact]
        public async Task AdicionarAsync_QuandoEntidadeInvalida_DeveRetornarBadRequest()
        {
            var atividade = _fixtures.Build<AtividadeViewModel>()
                                        .With(x => x.Nome, string.Empty)
                                        .Create();

            var result = await _atividadeService.AdicionarAsync(atividade);

            result.IsValid.Should().BeFalse();
            result.StatusCode.Should().Be(StatusCodeResultEnum.BadRequest);
        }

        [Fact]
        public async Task AtualizarAsync_QuandoEntidadeValida_DeveRetornarOk()
        {
            var atividadeExistente = _fixtures.Create<Atividade>();
            AssignHelper.ToProperty(_faker.Name.FullName(), nameof(Atividade.Nome), atividadeExistente);
            AssignHelper.ToProperty(_faker.Lorem.Text(), nameof(Atividade.Descricao), atividadeExistente);
            AssignHelper.ToProperty(_faker.PickRandom<TipoDeAtividade>(), nameof(Atividade.TipoAtividade), atividadeExistente);
            _atividadeRepository.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(atividadeExistente);
            var atualizacoesAtividade = _fixtures.Create<AtividadeViewModel>();

            var result = await _atividadeService.AtualizarAsync(atividadeExistente.Id, atualizacoesAtividade);

            result.StatusCode.Should().Be(StatusCodeResultEnum.Ok);
            result.Data.Nome.Should().Be(atualizacoesAtividade.Nome);
            result.Data.Descricao.Should().Be(atualizacoesAtividade.Descricao);
            result.Data.TipoAtividade.Should().Be(atualizacoesAtividade.TipoAtividade);
        }

        [Fact]
        public async Task AtualizarAsync_QuandoEntidadeInvalida_DeveRetornarBadRequestComMensagem()
        {

            var atualizacoesAtividade = _fixtures.Build<AtividadeViewModel>()
                                                    .With(x => x.Nome, string.Empty)
                                                    .Create();

            var result = await _atividadeService.AtualizarAsync(Guid.NewGuid(), atualizacoesAtividade);

            result.StatusCode.Should().Be(StatusCodeResultEnum.BadRequest);
            result.IsValid.Should().BeFalse();
            result.Message.Should().NotBeEmpty();
        }

        [Fact]
        public async Task AtualizarAsync_QuandoObterPorIdRetornaNull_DeveRetornarNotFoundComMensagem()
        {

            var atualizacoesAtividade = _fixtures.Create<AtividadeViewModel>();

            var result = await _atividadeService.AtualizarAsync(Guid.NewGuid(), atualizacoesAtividade);

            result.StatusCode.Should().Be(StatusCodeResultEnum.NotFound);
            result.IsValid.Should().BeFalse();
            result.Message.Should().Contain(Constantes.AtividadeNaoEncontradaErrorMsg);
        }

        [Fact]
        public async Task AtualizarAsync_QuandoObterPorIdRetorna500_DeveRetornarInternalServerErrorComNotificacao()
        {
            _atividadeRepository.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>()))
                                    .ThrowsAsync(new Exception());

            var atualizacoesAtividade = _fixtures.Create<AtividadeViewModel>();

            var result = await _atividadeService.AtualizarAsync(Guid.NewGuid(), atualizacoesAtividade);

            result.StatusCode.Should().Be(StatusCodeResultEnum.InternalServerError);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task RemoverAsync_QuandoIdValido_RetornaOk()
        {
            var atividadeExistente = _fixtures.Create<Atividade>();
            AssignHelper.ToProperty(_faker.Name.FullName(), nameof(Atividade.Nome), atividadeExistente);
            AssignHelper.ToProperty(_faker.Lorem.Text(), nameof(Atividade.Descricao), atividadeExistente);
            AssignHelper.ToProperty(_faker.PickRandom<TipoDeAtividade>(), nameof(Atividade.TipoAtividade), atividadeExistente);
            _atividadeRepository.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(atividadeExistente);

            var result = await _atividadeService.RemoverAsync(atividadeExistente.Id);

            result.StatusCode.Should().Be(StatusCodeResultEnum.Ok);
            result.Data.Should().BeEquivalentTo(atividadeExistente);
            result.Message.Should().Contain(Constantes.AtividadeRemovidaMsg);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task RemoverAsync_QuandoIdInvalido_RetornaNotFound()
        {
            var result = await _atividadeService.RemoverAsync(Guid.NewGuid());

            result.StatusCode.Should().Be(StatusCodeResultEnum.NotFound);
            result.Message.Should().Contain(Constantes.AtividadeNaoEncontradaErrorMsg);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task RemoverAsync_QuandoServerComErro_RetornaInternalServerErro()
        {
            var atividadeExistente = _fixtures.Create<Atividade>();
            AssignHelper.ToProperty(_faker.Name.FullName(), nameof(Atividade.Nome), atividadeExistente);
            AssignHelper.ToProperty(_faker.Lorem.Text(), nameof(Atividade.Descricao), atividadeExistente);
            AssignHelper.ToProperty(_faker.PickRandom<TipoDeAtividade>(), nameof(Atividade.TipoAtividade), atividadeExistente);
            _atividadeRepository.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(atividadeExistente);
            _atividadeRepository.Setup(x => x.RemoverAsync(It.IsAny<Guid>()))
                                                .ThrowsAsync(new Exception());

            var result = await _atividadeService.RemoverAsync(Guid.NewGuid());

            result.StatusCode.Should().Be(StatusCodeResultEnum.InternalServerError);
            result.IsValid.Should().BeFalse();
        }
    }
}