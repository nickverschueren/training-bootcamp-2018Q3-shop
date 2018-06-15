using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Shop.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IConfigurationRoot ConfigurationRoot { get; private set; }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigureDelegate)
                .UseStartup<Startup>()
                .Build();

        private static void ConfigureDelegate(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .AddJsonFile($"appSettings.{context.HostingEnvironment.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
        }
    }
}
