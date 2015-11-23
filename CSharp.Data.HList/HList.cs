using System;
using Microsoft.FSharp.Core;

namespace CSharp.Data
{
    public abstract class HList<A> where A : HList<A>
    {
        internal HList() {}

        public abstract HCons<E, A> Extend<E>(E e);
        public abstract Apply<Unit, Tuple<E, A>, HCons<E, A>> Extender<E>();
        public abstract int Length { get; }
    }

    public static class HList
    {
        public static HCons<E, L> Cons<E, L>(E e, L l) where L : HList<L>
        {
            return new HCons<E, L>(e, l);
        }

        private static readonly HNil nil = new HNil();
        public static HNil Nil() { return nil; }

        public static HCons<E, HNil> Single<E>(E e)
        {
            return new HCons<E, HNil>(e, Nil());
        }
    }

    public class HNil : HList<HNil>
    {
        internal HNil() {}

        public override HCons<E, HNil> Extend<E>(E e)
        {
            return HList.Cons(e, this);
        }

        public override Apply<Unit, Tuple<E, HNil>, HCons<E, HNil>> Extender<E>()
        {
            return Apply.Cons<E, HNil>();
        }

        public override int Length { get { return 0; } }
    }

    public class HCons<E, L> : HList<HCons<E, L>> where L : HList<L>
    {
        internal HCons(E e, L l)
        {
            Head = e;
            Tail = l;
            Length = l.Length + 1;
        }

        public override HCons<X, HCons<E, L>> Extend<X>(X e)
        {
            return HList.Cons(e, this);
        }

        public override Apply<Unit, Tuple<X, HCons<E, L>>, HCons<X, HCons<E, L>>> Extender<X>()
        {
            return Apply.Cons<X, HCons<E, L>>();
        }

        public E Head { get; }

        public L Tail { get; }

        public override int Length { get; }
    }
}
