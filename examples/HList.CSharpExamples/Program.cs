using System;
using CSharp.Data;

namespace CSharpExamples
{
    class Program
    {
        static void Main(string[] args)
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
    }
}
