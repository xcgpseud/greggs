using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Constants;
using Greggs.Products.Api.DataAccess.Interfaces;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services.Interfaces;

namespace Greggs.Products.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductAccess _productAccess;
    private readonly ICurrencyAccess _currencyAccess;

    public ProductService(IProductAccess productAccess, ICurrencyAccess currencyAccess)
    {
        _productAccess = productAccess;
        _currencyAccess = currencyAccess;
    }

    public IEnumerable<Product> GetProducts(
        int pageNumber = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize,
        string currencyName = Currencies.Gbp
    )
    {
        var products = _productAccess.List(pageNumber, pageSize);
        var currency = _currencyAccess.FindCurrencyByName(currencyName);

        if (currency == null)
        {
            // I could either throw an exception here and respond with a 404 upon invalid currency
            // Or I could gracefully fail and just use the default currency
            // I opted for the latter, but wanted to point out both options, as this would change depending on the goal
            return products;
        }

        // We're just using GBP as a default base currency so this seems to make sense for now
        return currencyName == Currencies.Gbp
            ? products
            : products.Select(product => ConvertCurrency(product, currency));
    }

    // This is a private static method now, but I'd make this a global helper which just focuses on the price conversion
    // Since this is going to be a private method which does the cast anyway, I just left it all here.
    private static Product ConvertCurrency(Product product, Currency currency)
    {
        // Round UP to the nearest 2 decimal places every time
        product.PriceValue = Math.Round(
            product.PriceValue * currency.AmountPerPound,
            2,
            MidpointRounding.ToPositiveInfinity
        );
        product.PriceUnit = currency.Name;

        return product;
    }
}