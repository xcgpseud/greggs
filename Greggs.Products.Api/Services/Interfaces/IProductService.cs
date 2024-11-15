using System.Collections.Generic;
using Greggs.Products.Api.Constants;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services.Interfaces;

public interface IProductService
{
    public IEnumerable<Product> GetProducts(int pageStart = -1, int pageSize = -1, string currencyName = Currencies.Gbp);
}