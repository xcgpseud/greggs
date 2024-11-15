namespace Greggs.Products.Api.Models;

/// <summary>
/// A Greggs product
/// </summary>
public class Product
{
    /// <summary>
    /// The name of the product
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The unit in which the price is measured
    /// </summary>
    public string PriceUnit { get; set; }
    
    /// <summary>
    /// The value of the item in the PriceUnit currency
    /// </summary>
    public decimal PriceValue { get; set; }
}