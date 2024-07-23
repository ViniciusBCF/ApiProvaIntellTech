using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProvaIntellTechApi.Configuration;
using ProvaIntellTechApi.Tests._Config;

namespace ProvaIntellTechApi.Tests
{
    [Trait("Aplicação", "Injeção de dependência")]
    public class DependencyInjectionTests
    {
        [Fact]
        public void DeveRegistrarTodasDependencias()
        {
            var basePath = ObterCaminhoBase();
            var hostingEnvironment = ObterHostingEnvironment(basePath);
            var configuration = CarregarConfiguracao(hostingEnvironment.ContentRootPath);
            var services = new ServiceCollection();
            ConfigurarServicos(services, configuration);
            var serviceProvider = ConstruirProvedorServico(services, hostingEnvironment);

            var excludedNamespaces = new List<string>
            {
                "Microsoft",
                "System"
            };

            var actualServices = ObterServicosRegistrados(serviceProvider, services, excludedNamespaces);

            VerificarTodosServicosEstaoRegistrados(actualServices);
        }

        private CustomHostingEnvironment ObterHostingEnvironment(string basePath)
        {
            return new CustomHostingEnvironment
            {
                ContentRootPath = basePath,
            };
        }

        private IConfiguration CarregarConfiguracao(string basePath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private void ConfigurarServicos(IServiceCollection services, IConfiguration configuration)
        {
            DependencyInjectionConfig.ConfigureDb(services, configuration);
            services.AddRepositories();
            services.AddServices();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Adicione outros serviços necessários, como logging, autenticação, etc.
            services.AddLogging();
            services.AddSingleton(configuration);
        }

        private IServiceProvider ConstruirProvedorServico(IServiceCollection services, CustomHostingEnvironment hostingEnvironment)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(hostingEnvironment)
                .As<IHostingEnvironment>()
                .SingleInstance();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        private List<object> ObterServicosRegistrados(IServiceProvider serviceProvider, IServiceCollection services, List<string> excludedNamespaces)
        {
            var registeredServices = new List<object>();
            foreach (var serviceDescriptor in services)
            {
                if (!ServicoEstaExcluido(serviceDescriptor, excludedNamespaces))
                {
                    var serviceType = serviceDescriptor.ServiceType;
                    var serviceInstance = serviceProvider.GetService(serviceType);
                    registeredServices.Add(serviceInstance);
                }
            }

            return registeredServices.Where(x => x != null).ToList();
        }

        private bool ServicoEstaExcluido(ServiceDescriptor serviceDescriptor, List<string> excludedNamespaces)
        {
            var serviceNamespace = serviceDescriptor.ServiceType.Namespace;
            return excludedNamespaces.Any(ns => serviceNamespace != null && serviceNamespace.StartsWith(ns));
        }

        private void VerificarTodosServicosEstaoRegistrados(List<object> actualServices)
        {
            foreach (var serviceInstance in actualServices)
            {
                Assert.NotNull(serviceInstance);
                Assert.IsAssignableFrom(serviceInstance.GetType(), serviceInstance);
            }
        }

        private string ObterCaminhoBase()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            while (directory != null)
            {
                DirectoryInfo sharedDirectory = new DirectoryInfo(Path.Combine(directory.FullName));

                if (sharedDirectory.Exists)
                {
                    return sharedDirectory.FullName;
                }

                directory = directory.Parent;
            }

            return null;
        }
    }
}
