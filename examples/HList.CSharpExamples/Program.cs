using System;
using CSharp.Data;
using Microsoft.FSharp.Core;

namespace CSharpExamples
{
    class Program
    {
        static void Main(string[] args)
        {
          AppendExample();
          FoldrExample();
        }

        static void AppendExample()
        {
            var a = HList.Nil().Extend(true).Extend(3).Extend("Foo");
            var b = HList.Nil().Extend(new [] { 1, 2 }).Extend("Bar").Extend(4.0);

            var zero = HAppend.Append<HCons<double, HCons<string, HCons<int[], HNil>>>>();
            var one = HAppend.Append<bool, HNil,
                HCons<double, HCons<string, HCons<int[], HNil>>>,
                HCons<double, HCons<string, HCons<int[], HNil>>>,
                HAppend<HNil, HCons<double, HCons<string, HCons<int[], HNil>>>,HCons<double, HCons<string, HCons<int[], HNil>>>>>(zero);
            var two = HAppend.Append<int,
                HCons<bool, HNil>,
                HCons<double, HCons<string, HCons<int[], HNil>>>,
                HCons<bool, HCons<double, HCons<string, HCons<int[], HNil>>>>,
                HAppend<HCons<bool, HNil>,
                    HCons<double, HCons<string, HCons<int[], HNil>>>,
                    HCons<bool, HCons<double, HCons<string, HCons<int[], HNil>>>>>>(one);
            var three = HAppend.Append<string,
                HCons<int, HCons<bool, HNil>>,
                HCons<double, HCons<string, HCons<int[], HNil>>>,
                HCons<int, HCons<bool, HCons<double, HCons<string, HCons<int[], HNil>>>>>,
                HAppend<HCons<int, HCons<bool, HNil>>,
                    HCons<double, HCons<string, HCons<int[], HNil>>>,
                    HCons<int, HCons<bool, HCons<double, HCons<string, HCons<int[], HNil>>>>>>>(two);

            var x = three.Append(a, b);

            Console.WriteLine(x.Head);
            Console.WriteLine(x.Tail.Tail.Tail.Tail.Head);
        }

        static void FoldrExample()
        {
          var functions = HList.Single<Func<int, int>>(x => x + 1)
              .Extend<Func<int, int>>(y => y * 2)
              .Extend<Func<string, int>>(z => z.Length);

          var comp1 = Apply.Comp<string, int, int>();
          var comp0 = Apply.Comp<int, int, int>();
          var fold0 = HFoldr.Hfoldr<Unit, Func<int, int>>();
          var fold2 = HFoldr.Hfoldr<Unit, Func<int, int>, Func<string, int>,
              HCons<Func<int, int>, HCons<Func<int, int>, HNil>>,
              Func<int, int>, Func<string, int>,
              Apply<Unit, Tuple<Func<string, int>, Func<int, int>>, Func<string, int>>,
              HFoldr<Unit, Func<int, int>, HCons<Func<int, int>, HCons<Func<int, int>, HNil>>, Func<int, int>>>(comp1,
                  HFoldr.Hfoldr<Unit, Func<int, int>, Func<int, int>, HCons<Func<int, int>, HNil>, Func<int, int>, Func<int, int>,
                      Apply<Unit, Tuple<Func<int, int>, Func<int, int>>, Func<int, int>>,
                      HFoldr<Unit, Func<int, int>, HCons<Func<int, int>, HNil>, Func<int, int>>>(comp0,
                          HFoldr.Hfoldr<Unit, Func<int, int>, Func<int, int>, HNil, Func<int, int>, Func<int, int>,
                              Apply<Unit, Tuple<Func<int, int>, Func<int, int>>, Func<int, int>>,
                              HFoldr<Unit, Func<int, int>, HNil, Func<int, int>>>(comp0, fold0)));
          Func<int, int> id = i => i;

          Console.WriteLine(fold2.FoldRight(null, id, functions)("abc"));
        }
    }
}
