using System;
using System.Linq;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Greggs.Products.Api.Services.Interfaces;
using NUnit.Framework;

namespace Greggs.Products.UnitTests.ControllerTests;

[TestFixture]
public class ProductControllerTests : TestBase
{
    private IProductService _productService;
    private ProductController _sut;

    [SetUp]
    public void SetUp()
    {
        _productService = A.Fake<IProductService>();
        _sut = new ProductController(_productService);
    }

    // As this endpoint just returns a list, a "not found" seemed unnecessary - everything fails
    // quietly and continues with no results if none should be found.
    // Thus, I've not added response codes other than 200.
    [Test]
    public void Get_WhenNoProductsExist_ReturnsEmptyArray()
    {
        // ARRANGE
        A.CallTo(() => _productService.GetProducts(A<int>._, A<int>._, A<string>._))
            .Returns(Array.Empty<Product>());

        // ACT
        var result = _sut.Get();

        // ASSERT
        result.Should().BeEmpty();
    }
}