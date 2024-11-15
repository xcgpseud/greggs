using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess.Interfaces;

public interface ICurrencyAccess : IDataAccess<Currency>
{
    public Currency FindCurrencyByName(string name);
}