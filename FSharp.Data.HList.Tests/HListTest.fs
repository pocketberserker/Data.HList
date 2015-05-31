namespace FSharp.Data.HList.Tests

open Persimmon
open UseTestNameByReflection
open FSharp.Data

module HListTest =

  let ``nil cons head`` = test {
    do! assertEquals true (HList.cons true HList.nil |> HList.head)
  }

  let ``cons cons tail head`` = test {
    do! assertEquals 1 (true + (1 + HList.nil) |> HList.tail |> HList.head)
  }

  let ``append test`` = test {
    let a = HList.nil |> HList.cons true |> HList.cons 3 |> HList.cons "Foo"
    let b = HList.nil |> HList.cons [1; 2] |> HList.cons "Bar" |> HList.cons 4.0
    let zero = HAppend.define
    let one = HAppend.append zero
    let two = HAppend.append one
    let three = HAppend.append two
    let x = HList.append three a b
    do! assertEquals "Foo" (HList.head x)
    do! assertEquals "Bar" (x |> HList.tail |> HList.tail |> HList.tail |> HList.tail |> HList.head)
  }
