using System;
using System.Collections.Generic;
using System.Linq;


namespace TAlex.Common.Extensions
{
    public static class MaybeMonadExtensions
    {
        public static TResult With<TInput, TResult>(this TInput source, Func<TInput, TResult> evaluator)
            where TInput : class where TResult : class
        {
            if (source == null) return null;
            return evaluator(source);
        }

        public static TInput If<TInput>(this TInput source, Predicate<TInput> predicate)
            where TInput : class
        {
            if (source == null) return null;
            return predicate(source) ? source : null;
        }

        public static TInput Do<TInput>(this TInput source, Action<TInput> action)
            where TInput : class
        {
            if (source == null) return null;
            action(source);
            return source;
        }
    }
}
