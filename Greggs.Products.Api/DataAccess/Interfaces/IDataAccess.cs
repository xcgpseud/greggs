using System.Collections.Generic;
using Greggs.Products.Api.Constants;

namespace Greggs.Products.Api.DataAccess.Interfaces;

public interface IDataAccess<out T>
{
    IEnumerable<T> List(
        int pageNumber = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize
    );
}