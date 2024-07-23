using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace ProvaIntellTechApi.Tests._Config
{
    public class CustomHostingEnvironment : IHostingEnvironment
    {
        public string EnvironmentName { get; set; }
        public string ApplicationName { get; set; }
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
