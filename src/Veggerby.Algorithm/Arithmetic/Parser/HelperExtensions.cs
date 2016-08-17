using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public static class HelperExtensions
    {
        public static IEnumerable<T> Before<T>(this IEnumerable<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));                
            }

            return source.TakeWhile(x => !item.Equals(x));
        }

        public static IEnumerable<T> After<T>(this IEnumerable<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));                
            }

            return source
                .SkipWhile(x => !item.Equals(x)) // skip until item
                .Skip(1); // skip item
        }

        public static T Previous<T>(this IEnumerable<T> source, T item)
        {
            return source.Before(item).LastOrDefault();
        }

        public static T Next<T>(this IEnumerable<T> source, T item)
        {
            return source.After(item).FirstOrDefault();
        }
    }
}