using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace System.Reactive.Linq.ToAwait
{
    public static class LinqToAwait
    {
        public static IObservable<TRet> SelectAsync<T, TRet>(this IObservable<T> This, Func<T, Task<TRet>> selector, bool preserveOrdering = false)
        {
            return preserveOrdering ?
                This.Select(x => selector(x).ToObservable()).Concat() :
                This.SelectMany(x => selector(x).ToObservable());
        }

        public static IObservable<T> WhereAsync<T>(this IObservable<T> This, Func<T, Task<bool>> filter)
        {
            return This.Select(x => {
                return filter(x).ToObservable()
                    .SelectMany(cond => cond ? Observable.Return(x) : Observable.Empty<T>());
            }).Concat();
        }

        public static IObservable<bool> AllAsync<T>(this IObservable<T> This, Func<T, Task<bool>> filter)
        {
            return This
                .SelectMany(x => filter(x).ToObservable())
                .All(x => x != false);
        }

        public static IObservable<bool> AnyAsync<T>(this IObservable<T> This, Func<T, Task<bool>> filter)
        {
            return This
                .SelectMany(x => filter(x).ToObservable())
                .Any(x => x != false);
        }

        public static IObservable<T> AsAsync<T>(this IEnumerable<T> This)
        {
            return This.ToObservable(TaskPoolScheduler.Default);
        }

        public static IObservable<IList<T>> GetResults<T>(this IObservable<T> This)
        {
            return This.ToList();
        }
    }
}