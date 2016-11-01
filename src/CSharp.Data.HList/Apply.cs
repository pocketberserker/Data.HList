using System;
using Microsoft.FSharp.Core;

namespace CSharp.Data
{
    public interface Apply<F, A, R>
    {
        R Apply(F f, A a);
    }

    public static class Apply
    {
        public static Apply<Unit, A, A> Id<A>()
        {
            return new IdApply<A>();
        }

        public static Apply<Func<X, Y>, X, Y> Func<X, Y>()
        {
            return new FuncApply<X, Y>();
        }

        public static Apply<Unit, Tuple<Func<X, Y>, Func<Y, Z>>, Func<X, Z>> Comp<X, Y, Z>()
        {
            return new CompApply<X, Y, Z>();
        }

        public static Apply<Unit, Tuple<E, L>, HCons<E, L>> Cons<E, L>() where L : HList<L>
        {
            return new ConsApply<E, L>();
        }

        public static Apply<HAppend<A, B, C>, Tuple<A, B>, C> Append<A, B, C>()
        {
            return new HAppendApply<A, B, C>();
        }
    }

    sealed class IdApply<A> : Apply<Unit, A, A>
    {
        public A Apply(Unit f, A a)
        {
            return a;
        }
    }

    sealed class FuncApply<X, Y> : Apply<Func<X, Y>, X, Y>
    {
        public Y Apply(Func<X, Y> f, X a)
        {
            return f(a);
        }
    }

    sealed class CompApply<X, Y, Z> : Apply<Unit, Tuple<Func<X, Y>, Func<Y, Z>>, Func<X, Z>>
    {
        public Func<X, Z> Apply(Unit f, Tuple<Func<X, Y>, Func<Y, Z>> a)
        {
            Func<X, Z> impl = x => a.Item2(a.Item1(x));
            return impl;
        }
    }

    sealed class ConsApply<E, L> : Apply<Unit, Tuple<E, L>, HCons<E, L>> where L : HList<L>
    {
        public HCons<E, L> Apply(Unit f, Tuple<E, L> a)
        {
            return HList.Cons<E, L>(a.Item1, a.Item2);
        }
    }

    sealed class HAppendApply<A, B, C> : Apply<HAppend<A, B, C>, Tuple<A, B>, C>
    {
        public C Apply(HAppend<A, B, C> f, Tuple<A, B> a)
        {
            return f.Append(a.Item1, a.Item2);
        }
    }
}
