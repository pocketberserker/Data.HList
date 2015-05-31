namespace FSharp.Data

open CSharp.Data

module HList =

  let nil = HList.Nil()

  let inline cons e (l: #HList<_>) = l.Extend(e)

  let inine singleton e = HList.Single(e)

  let inline head (c: HCons<_, _>) = c.Head
  let inline tail (c: HCons<_, _>) = c.Tail

  let inline append (h: #HAppend<_, _, _>) a b = h.Append(a, b)

module HAppend =

  let define<'T when 'T :> HList<'T>> = HAppend.Append<'T>()

  let inline append (h: HAppend<_, _, _>) = HAppend.Append(h)

[<AutoOpen>]
module HListSyntax =

  let (+) e l = HList.cons e l
