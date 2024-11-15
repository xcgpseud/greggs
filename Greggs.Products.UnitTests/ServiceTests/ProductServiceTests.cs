using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Greggs.Products.Api.Constants;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using NUnit.Framework;

namespace Greggs.Products.UnitTests.ServiceTests;

[TestFixture]
public class ProductServiceTests : TestBase
{
    // Again breaking some convention and using the direct class signatures here so that I can
    // interact with the ProductDatabase field. I'd usually just implement the interfaces
    // as I wouldn't care about this property and mocking it.

    // Note: With some more time I may have moved the actual data out to its own class and mocked that
    // So that I could keep my Access mocks cleaner.
    private CurrencyAccess _currencyAccess;
    private ProductAccess _productAccess;

    private ProductService _sut;

    [SetUp]
    public void SetUp()
    {
        // So the DB is fake and all that stuff, but I still felt I should fake it in some way
        _currencyAccess = Fixture.Create<CurrencyAccess>();
        _productAccess = Fixture.Create<ProductAccess>();

        _sut = new ProductService(_productAccess, _currencyAccess);

        // Some preset, known values for these tests
        // Yes these are the same as our "actual" access layer but as long as they ae predictable for us
        // in these tests, I'm happy!
        _productAccess.ProductDatabase = new List<Product>
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
    }

    [TestCase(1, 4, 4)]
    [TestCase(2, 4, 4)]
    [TestCase(3, 4, 0)]
    [TestCase(2, 5, 3)]
    [TestCase(-1, 3, 3)]
    [TestCase(-1, 10, 8)]
    [TestCase(2, -1, 3)]
    public void GetProducts_WithPageDataSet_AndNoCurrency_PaginatesCorrectly(
        int pageStart,
        int pageSize,
        int expectedCount
    )
    {
        // ARRANGE
        // ACT
        var result = _sut.GetProducts(pageStart, pageSize);

        // ASSERT
        result.Count().Should().Be(expectedCount);
    }

    [Test]
    public void GetProducts_WithNoParameters_ReturnsDefaultFirstPage()
    {
        // ARRANGE
        // ACT
        var result = _sut.GetProducts().ToArray();

        // ASSERT
        result.Should().NotBeEmpty();

        // Hard coded values because they are in the code and not elsewhere.
        // A change to these would fail a test, forcing the dev to spot this side effect.
        // Not something I'd usually do with something that can change, but in this case why not!?
        result.Length.Should().Be(5);
        result.First().Name.Should().Be("Sausage Roll");
        result.Last().Name.Should().Be("Pink Jammie");
    }

    [Test]
    public void GetProducts_WithEuroCurrency_ConvertsCurrencyCorrectly()
    {
        // ARRANGE
        const decimal euroConversionRate = 1.11m;
        var defaultValues = _sut.GetProducts();

        // ACT
        var result = _sut.GetProducts(-1, -1, Currencies.Eur);

        // ASSERT
        result.Should().AllSatisfy(
            product =>
            {
                var originalProduct = defaultValues.First(p => p.Name == product.Name);

                product.PriceUnit.Should().Be(Currencies.Eur);
                Math.Floor(product.PriceValue)
                    .Should()
                    .Be(Math.Floor(originalProduct.PriceValue * euroConversionRate));
            }
        );
    }

    [Test]
    public void GetProducts_WithInvalidCurrency_ReturnsGbpProducts()
    {
        // ARRANGE
        var defaultValues = _sut.GetProducts();

        // ACT
        var result = _sut.GetProducts(-1, -1, "Fake");

        // ASSERT
        result.Should().AllSatisfy(
            product => { product.Should().Be(defaultValues.First(p => p.Name == product.Name)); }
        );
    }
}