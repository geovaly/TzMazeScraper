using System.Collections.Generic;
using System.Linq;

namespace TzMazeScraper.Utils
{
    public static class PagingUtil
    {
        public const int FirstPageNumber = 0;

        public static IEnumerable<T> Page<T>(this IEnumerable<T> enumerable, int pageSize, int pageNumber)
        {
            return enumerable.Skip((pageNumber - FirstPageNumber) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> enumerable, int pageSize, int pageNumber)
        {
            return enumerable.Skip((pageNumber - FirstPageNumber) * pageSize).Take(pageSize);
        }
    }
}
