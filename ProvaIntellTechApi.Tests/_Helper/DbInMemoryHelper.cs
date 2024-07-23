using Microsoft.EntityFrameworkCore;
using ProvaIntellTechApi.Data.Context;

namespace ProvaIntellTechApi.Tests._Helper
{
    public static class DbInMemoryHelper
    {
        public static DbContextOptions<AppDbContext> CriarOpcoesInMemory()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}
