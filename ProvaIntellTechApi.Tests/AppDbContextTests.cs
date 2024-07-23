using AutoFixture;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProvaIntellTechApi.Data.Context;
using ProvaIntellTechApi.Domain.Entities;
using ProvaIntellTechApi.Tests._Helper;

namespace ProvaIntellTechApi.Tests
{
    public class AppDbContextTests
    {
        private readonly Fixture _fixtures;
        private readonly Faker _faker;

        public AppDbContextTests()
        {
            _fixtures = new Fixture();
            _faker = new Faker();
        }

        private async Task<Atividade> AdicionarAtividadeAsync(AppDbContext context)
        {
            var atividade = _fixtures.Create<Atividade>();
            context.Atividades.Add(atividade);
            await context.SaveChangesAsync();
            return atividade;
        }

        [Fact]
        public async Task SaveChangesAsync_DeveSetarDataCriacao()
        {
            var options = DbInMemoryHelper.CriarOpcoesInMemory();
            using var context = new AppDbContext(options);
            var atividade = _fixtures.Create<Atividade>();
            context.Atividades.Add(atividade);

            await context.SaveChangesAsync();

            atividade.DataCriacao.Should().NotBe(default);
        }

        [Fact]
        public async Task SaveChangesAsync_DeveSetarDataAtualizacao()
        {
            var options = DbInMemoryHelper.CriarOpcoesInMemory();
            using var context = new AppDbContext(options);
            var atividade = await AdicionarAtividadeAsync(context);
            var nome = _faker.Name.FullName();

            atividade.AlterarNome(nome);
            context.Atividades.Update(atividade);
            await context.SaveChangesAsync();

            atividade.DataAtualizacao.Should().NotBe(default);
            atividade.DataCriacao.Should().BeSameDateAs((DateTime)context.Entry(atividade).Property("DataCriacao").CurrentValue);
        }
    }
}