using Greggs.Products.Api.Constants;

namespace Greggs.Products.Api.Helpers;

public static class PaginationHelper
{
    public static (int, int) GetActualPageData(int pageNumber, int pageSize)
    {
        var number = pageNumber < 0 ? Configuration.DefaultPageNumber : pageNumber;
        var size = pageSize;

        if (size < 0)
        {
            size = Configuration.DefaultPageSize;
        }

        if (size > Configuration.MaxPageSize)
        {
            size = Configuration.MaxPageSize;
        }

        return (number, size);
    }
}