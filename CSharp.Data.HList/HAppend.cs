using System;

namespace CSharp.Data
{
    public class HAppend<A, B, C>
    {
        private Func<A, B, C> append;

        internal HAppend(Func<A, B, C> f)
        {
            append = f;
        }

        public C Append(A a, B b)
        {
            return append(a, b);
        }
    }

    public static class HAppend
    {
        public static HAppend<HNil, L, L> Append<L>() where L : HList<L>
        {
            return new HAppend<HNil, L, L>((nil, l) => l);
        }

        public static HAppend<HCons<X, A>, B, HCons<X, C>> Append<X, A, B, C, H>(H h)
            where A : HList<A>
            where C : HList<C>
            where H : HAppend<A, B, C>
        {
            return new HAppend<HCons<X, A>, B, HCons<X, C>>((c, l) => HList.Cons(c.Head, h.Append(c.Tail, l)));
        }
    }
}
