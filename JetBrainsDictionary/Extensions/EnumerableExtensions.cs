using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetBrainsDictionary.Extensions
{
    static class EnumerableExtensions
    {
        public static Task<T[]> ToArrayAsync<T>(this IEnumerable<T> ts) => Task.Run(() => ts.ToArray());

        public static Task<int> CountAsync<T>(this IEnumerable<T> ts) => Task.Run(() => ts.Count());
    }
}
