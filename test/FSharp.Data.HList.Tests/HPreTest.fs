module FSharp.Data.Tests.HPreTest

open Persimmon
open UseTestNameByReflection
open FSharp.Data.HPre


let ``a + 0 = a`` = test {
  let a = HNat.hsucc(HNat.hsucc(HNat.hsucc(HNat.hzero)))
  do! assertEquals (HNat.toInt a) ((HAdd.add(a, HNat.hzero)).Sum |> HNat.toInt)
}
