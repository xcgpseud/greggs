using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Constants;
using Greggs.Products.Api.DataAccess.Interfaces;
using Greggs.Products.Api.Helpers;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess;

/// <summary>
/// DISCLAIMER: This is only here to help enable the purpose of this exercise, this doesn't reflect the way we work!
/// </summary>
public class ProductAccess : IProductAccess
{
    // I would never normally do something like this, as an FYI, but:
    // I made this public and non-static so I can mock it in the tests.
    // Usually I would do this properly, with a mocked context factory and manage it
    // that way, however since we already have an unconventional data access layer
    // I've kept it unconventional! ;)
    public IEnumerable<Product> ProductDatabase = new List<Product>()
    {
        new() { Name = "Sausage Roll", PriceValue = 1m, PriceUnit = Currencies.Gbp },
        new() { Name = "Vegan Sausage Roll", PriceValue = 1.1m, PriceUnit = Currencies.Gbp },
        new() { Name = "Steak Bake", PriceValue = 1.2m, PriceUnit = Currencies.Gbp },
        new() { Name = "Yum Yum", PriceValue = 0.7m, PriceUnit = Currencies.Gbp },
        new() { Name = "Pink Jammie", PriceValue = 0.5m, PriceUnit = Currencies.Gbp },
        new() { Name = "Mexican Baguette", PriceValue = 2.1m, PriceUnit = Currencies.Gbp },
        new() { Name = "Bacon Sandwich", PriceValue = 1.95m, PriceUnit = Currencies.Gbp },
        new() { Name = "Coca Cola", PriceValue = 1.2m, PriceUnit = Currencies.Gbp },
    };

    // With a proper implementation I'd use some configurable default values for this, rather than
    // hardcoded ones. This is to avoid returning hundreds of values with a larger list
    public IEnumerable<Product> List(
        int pageNumber = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize
    )
    {
        var queryable = ProductDatabase.AsQueryable();

        var (number, size) = PaginationHelper.GetActualPageData(pageNumber, pageSize);

        queryable = queryable.Skip((number - 1) * size);
        queryable = queryable.Take(size);

        return queryable.ToList();
    }
}