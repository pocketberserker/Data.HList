namespace FSharp.Data

open CSharp.Data

module HList =

  let nil = HList.Nil()

  let inline cons e (l: #HList<_>) = l.Extend(e)

  let inline singleton e = HList.Single(e)

  let inline head (c: HCons<_, _>) = c.Head
  let inline tail (c: HCons<_, _>) = c.Tail

  let inline append (h: #HAppend<_, _, _>) a b = h.Append(a, b)

  let inline length (h: HList<_>) = h.Length

  let inline foldBack (foldr: HFoldr<_, _, _, _>) f v l = foldr.FoldRight(f, v, l)

module HAppend =

  let define<'T when 'T :> HList<'T>> = HAppend.Append<'T>()

  let inline append (h: HAppend<_, _, _>) = HAppend.Append(h)

module Apply =

  let id<'A> = Apply.Id<'A>()

  let func = { new Apply<_ -> _, _, _> with
    member __.Apply(f, a) = f a }

  let comp = { new Apply<unit, (_ -> _) * (_ -> _), _ -> _> with
    member __.Apply((), (a1, a2)) = a1 >> a2 }

  let cons<'E, 'L when 'L :> HList<'L>> = { new Apply<unit, 'E * 'L, HCons<'E, 'L>> with
    member __.Apply((), (a1, a2)) = HList.cons a1 a2 }

  let append = { new Apply<HAppend<_, _, _>, _ * _, _> with
    member __.Apply(f, (a1, a2)) = f.Append(a1, a2) }

module HFoldBack =

  let define<'G, 'V> = HFoldr.Hfoldr<'G, 'V>()

  let inline foldBack p h = HFoldr.Hfoldr(p, h)

[<AutoOpen>]
module HListSyntax =

  let (+|) e l = HList.cons e l
