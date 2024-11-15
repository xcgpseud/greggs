using Greggs.Products.Api.IoC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Greggs.Products.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = CreateHostBuilder(args);

        builder.ConfigureServices(
            services =>
            {
                DataAccessServiceConfiguration.ConfigureServices(services);
                ServiceServiceConfiguration.ConfigureServices(services);
            }
        );
        
        builder.Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}