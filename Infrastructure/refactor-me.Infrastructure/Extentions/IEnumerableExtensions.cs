using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Infrastructure.Extentions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Execute Action to to iterate through collection in foreach
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForEachElement<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection ?? Enumerable.Empty<T>())
            {
                action(item);
            }
        }


        /// <summary>
        /// Execute Action to to iterate through collection in foreach
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Count() == 0;
        }
    }
}
