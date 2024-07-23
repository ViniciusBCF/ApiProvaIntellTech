using AutoMapper;
using ProvaIntellTechApi.Configuration;

namespace ProvaIntellTechApi.Tests
{
    [Trait("Aplicação", "Perfil de mapeamento")]
    public class MappingProfileTests
    {
        [Fact]
        public void DeveValidarMappingProfile()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperConfig()); });

            mapperConfig.AssertConfigurationIsValid();
        }
    }
}
