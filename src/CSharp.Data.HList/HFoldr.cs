using System;

namespace CSharp.Data
{
    public class HFoldr<G, V, L, R>
    {
        private Func<G, V, L, R> foldRight;

        internal HFoldr(Func<G, V, L, R> f)
        {
            foldRight = f;
        }

        public R FoldRight(G f, V v, L l)
        {
            return foldRight(f, v, l);
        }
    }

    public static class HFoldr
    {
        public static HFoldr<G, V, HNil, V> Hfoldr<G, V>()
        {
            return new HFoldr<G, V, HNil, V>((f, v, hnil) => v);
        }

        public static HFoldr<G, V, HCons<E, L>, RR> Hfoldr<G, V, E, L, R, RR, PP, H>(PP p, H h)
            where L : HList<L>
            where  H : HFoldr<G, V, L, R>
            where PP : Apply<G, Tuple<E, R>, RR>
        {
            return new HFoldr<G, V, HCons<E, L>, RR>((f, v, c) => p.Apply(f, Tuple.Create(c.Head, h.FoldRight(f, v, c.Tail))));
        }
    }
}
