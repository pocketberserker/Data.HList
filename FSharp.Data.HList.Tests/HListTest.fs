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
