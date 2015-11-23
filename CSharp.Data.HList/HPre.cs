namespace CSharp.Data
{
    public class HPre
    {
        private HPre() { }

        public class HBool
        {
            internal HBool() { }
        }

        public sealed class HTrue : HBool
        {
        }

        public sealed class HFalse : HBool
        {
        }

        private static HTrue htrue = new HTrue();
        public static HTrue Htrue { get { return htrue; } }

        private static HFalse hfalse = new HFalse();
        public static HFalse Hfalse { get { return hfalse; } }

        public sealed class HAnd<A, B, C>
            where A : HBool
            where B : HBool
            where C : HBool
        {
            internal HAnd(C v)
            {
                Value = v;
            }

            public C Value { get; }

            public static HAnd<HFalse, HFalse, HFalse> hand(HFalse a, HFalse b)
            {
                return new HAnd<HFalse, HFalse, HFalse>(hfalse);
            }

            public static HAnd<HTrue, HFalse, HFalse> hand(HTrue a, HFalse b)
            {
                return new HAnd<HTrue, HFalse, HFalse>(hfalse);
            }

            public static HAnd<HFalse, HTrue, HFalse> hand(HFalse a, HTrue b)
            {
                return new HAnd<HFalse, HTrue, HFalse>(hfalse);
            }

            public static HAnd<HTrue, HTrue, HTrue> hand(HTrue a, HTrue b)
            {
                return new HAnd<HTrue, HTrue, HTrue>(htrue);
            }
        }

        public sealed class HOr<A, B, C>
            where A : HBool
            where B : HBool
            where C : HBool
        {
            internal HOr(C v)
            {
                Value = v;
            }

            public C Value { get; }

            public static HAnd<HFalse, HFalse, HFalse> hor(HFalse a, HFalse b)
            {
                return new HAnd<HFalse, HFalse, HFalse>(hfalse);
            }

            public static HAnd<HTrue, HFalse, HTrue> hor(HTrue a, HFalse b)
            {
                return new HAnd<HTrue, HFalse, HTrue>(htrue);
            }

            public static HAnd<HFalse, HTrue, HTrue> hor(HFalse a, HTrue b)
            {
                return new HAnd<HFalse, HTrue, HTrue>(htrue);
            }

            public static HAnd<HTrue, HTrue, HTrue> hor(HTrue a, HTrue b)
            {
                return new HAnd<HTrue, HTrue, HTrue>(htrue);
            }
        }

        public sealed class HCond<T, X, Y, Z>
        {
            public HCond(Z z)
            {
                Value = z;
            }

            public Z Value { get; }

            public static HCond<HFalse, X, Y, Y> hcond(HFalse t, X x, Y y)
            {
                return new HCond<HFalse, X, Y, Y>(y);
            }

            public static HCond<HTrue, X, Y, X> hcond(HTrue t, X x, Y y)
            {
                return new HCond<HTrue, X, Y, X>(x);
            }
        }

        public abstract class HNat<A> where A : HNat<A>
        {
            public abstract int ToInt();
        }

        public sealed class HZero : HNat<HZero>
        {
            internal HZero() { }

            public override int ToInt()
            {
                return 0;
            }
        }

        public sealed class HSucc<N> : HNat<HSucc<N>> where N : HNat<N>
        {
            internal N pred;

            internal HSucc(N n)
            {
                this.pred = n;
            }

            public override int ToInt()
            {
                return 1 + pred.ToInt();
            }
        }

        public static class HNat
        {
            public static HZero hzero()
            {
                return new HZero();
            }

            public static HSucc<N> hsucc<N>(N n) where N : HNat<N>
            {
                return new HSucc<N>(n);
            }

            public static N hpred<N>(HSucc<N> n) where N : HNat<N>
            {
                return n.pred;
            }
        }

        public class HEq<X, Y, B> where B : HBool
        {
            internal HEq(B v)
            {
                Value = v;
            }

            public B Value { get; private set; }

            public static HEq<HZero, HZero, HTrue> eq(HZero a, HZero b)
            {
                return new HEq<HZero, HZero, HTrue>(htrue);
            }

            public static HEq<HZero, HSucc<N>, HFalse> eq<N>(HZero a, HSucc<N> b) where N : HNat<N>
            {
                return new HEq<HZero, HSucc<N>, HFalse>(hfalse);
            }

            public static HEq<HSucc<N>, HZero, HFalse> eq<N>(HSucc<N> a, HZero b) where N : HNat<N>
            {
                return new HEq<HSucc<N>, HZero, HFalse>(hfalse);
            }

            public static HEq<HSucc<N>, HSucc<NN>, B> eq<N, NN, E>(HSucc<N> a, HSucc<NN> b, E e)
                where N : HNat<N>
                where NN : HNat<NN>
                where E : HEq<N, NN, B>
            {
                return new HEq<HSucc<N>, HSucc<NN>, B>(e.Value);
            } 
        }

        public class HAdd<A, B, C> where A : HNat<A> where B : HNat<B> where C : HNat<C>
        {
            internal HAdd(C sum)
            {
                Sum = sum;
            }

            public C Sum { get; }
        }

        public static class HAdd
        {

            public static HAdd<HZero, HSucc<N>, HSucc<N>> add<N>(HZero a, HSucc<N> b) where N : HNat<N>
            {
                return new HAdd<HZero, HSucc<N>, HSucc<N>>(b);
            }

            public static HAdd<HSucc<N>, HZero, HSucc<N>> add<N>(HSucc<N> a, HZero b) where N : HNat<N>
            {
                return new HAdd<HSucc<N>, HZero, HSucc<N>>(a);
            }

            public static HAdd<HSucc<N>, HSucc<M>, HSucc<R>> add<N, M, R, H>(HSucc<N> a, HSucc<M> b, H h)
                where N : HNat<N>
                where M : HNat<M>
                where R : HNat<R>
                where H : HAdd<N, HSucc<M>, R>
            {
                return new HAdd<HSucc<N>, HSucc<M>, HSucc<R>>(HNat.hsucc(h.Sum));
            }
        }
    }
}
