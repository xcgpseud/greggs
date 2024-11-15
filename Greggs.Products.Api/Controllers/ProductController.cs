using System;
using System.Collections.Generic;
using Greggs.Products.Api.Constants;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Gets a list of products along with their price in the given currenc
    /// </summary>
    /// <param name="pageStart">The page we wish to load</param>
    /// <param name="pageSize">The size of our current pages</param>
    /// <param name="currency">The currency to display prices in. Will return GBP in case of incorrect currency</param>
    /// <returns>A list of products</returns>
    [HttpGet]
    public IEnumerable<Product> Get(
        int pageStart = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize,
        string currency = Currencies.Gbp
    )
    {
        return _productService.GetProducts(pageStart, pageSize, currency) ?? Array.Empty<Product>();
    }
}