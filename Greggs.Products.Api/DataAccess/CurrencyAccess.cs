using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Constants;
using Greggs.Products.Api.DataAccess.Interfaces;
using Greggs.Products.Api.Helpers;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess;

public class CurrencyAccess : ICurrencyAccess
{
    private static readonly IEnumerable<Currency> CurrencyDatabase = new List<Currency>
    {
        new() { Name = Currencies.Gbp, AmountPerPound = 1m },
        new() { Name = Currencies.Eur, AmountPerPound = 1.11m },
    };

    public Currency FindCurrencyByName(string name)
        => CurrencyDatabase.FirstOrDefault(x => x.Name == name);

    public IEnumerable<Currency> List(
        int pageNumber = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize
    )
    {
        var queryable = CurrencyDatabase.AsQueryable();

        var (number, size) = PaginationHelper.GetActualPageData(pageNumber, pageSize);

        queryable = queryable.Skip((number - 1) * size);
        queryable = queryable.Take(size);

        return queryable.ToList();
    }
}