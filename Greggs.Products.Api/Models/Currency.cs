namespace Greggs.Products.Api.Models;

/// <summary>
/// A financial currency
/// </summary>
public class Currency
{
    /// <summary>
    /// The name of the currency
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// What 1 GBP worth of this currency is worth
    /// </summary>
    public decimal AmountPerPound { get; init; }
}