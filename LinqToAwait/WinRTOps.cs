using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Windows.Foundation;
using Windows.Foundation;

namespace LinqToAwait
{
    public static class LinqToAwaitWinRT
    {
        public static IObservable<TRet> SelectAsync<T, TRet>(this IObservable<T> This, Func<T, IAsyncOperation<TRet>> selector, bool preserveOrdering = false)
        {
            return preserveOrdering ?
                This.Select(x => selector(x).ToObservable()).Concat() :
                This.SelectMany(x => selector(x).ToObservable());
        }
    }
}
