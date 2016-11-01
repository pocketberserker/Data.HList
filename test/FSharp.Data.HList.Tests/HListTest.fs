namespace FSharp.Data.HList.Tests

open Persimmon
open UseTestNameByReflection
open FSharp.Data

module HListTest =

  let ``head test`` = test {
    let a = HList.nil |> HList.cons true |> HList.cons 3 |> HList.cons "Foo"
    do! assertEquals "Foo" (a |> HList.head)
    return a
  }

  let ``tail test`` = test {
    let a = HList.nil |> HList.cons [1; 2] |> HList.cons "Bar" |> HList.cons 4.0
    do! assertEquals [1; 2] (a |> HList.tail |> HList.tail |> HList.head)
    return a
  }

  let ``append test`` = test {
    let! a = ``head test``
    let! b = ``tail test``
    let zero = HAppend.define
    let one = HAppend.append zero
    let two = HAppend.append one
    let three = HAppend.append two
    let x = HList.append three a b
    do! assertEquals "Foo" (HList.head x)
    do! assertEquals "Bar" (x |> HList.tail |> HList.tail |> HList.tail |> HList.tail |> HList.head)
    return x
  }

  let ``length test`` = test {
    let! a = ``head test``
    let! b = ``tail test``
    let! x = ``append test``
    do! assertEquals 0 (HList.length HList.nil)
    do! assertEquals 3 (HList.length a)
    do! assertEquals (HList.length a + HList.length b) (HList.length x)
  }

  let ``foldBack test`` = test {
    let functions = HList.singleton (fun x -> 1 + x) |> HList.cons (fun x -> 2 * x) |> HList.cons String.length
    let comp1 = Apply.comp
    let comp0 = Apply.comp
    let fold0 = HFoldBack.define
    let fold2 = HFoldBack.foldBack comp1 (HFoldBack.foldBack comp0 (HFoldBack.foldBack comp0 fold0))
    do! assertEquals 7 (HList.foldBack fold2 () id functions "abc")
  }

