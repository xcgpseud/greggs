using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Greggs.Products.Api.IoC;

public static class DataAccessServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IProductAccess, ProductAccess>();
        services.AddScoped<ICurrencyAccess, CurrencyAccess>();
    }
}