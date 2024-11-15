using Greggs.Products.Api.Services;
using Greggs.Products.Api.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Greggs.Products.Api.IoC;

public static class ServiceServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
    }
}