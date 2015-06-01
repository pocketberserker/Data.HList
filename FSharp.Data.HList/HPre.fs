module FSharp.Data.HPre

open CSharp.Data

type HBool =
  static member hand(a: HPre.HFalse, b: HPre.HFalse) = HPre.HAnd.hand(a, b)
  static member hand(a: HPre.HTrue, b: HPre.HFalse) = HPre.HAnd.hand(a, b)
  static member hand(a: HPre.HFalse, b: HPre.HTrue) = HPre.HAnd.hand(a, b)
  static member hand(a: HPre.HTrue, b: HPre.HTrue) = HPre.HAnd.hand(a, b)
  static member hor(a: HPre.HFalse, b: HPre.HFalse) = HPre.HOr.hor(a, b)
  static member hor(a: HPre.HTrue, b: HPre.HFalse) = HPre.HOr.hor(a, b)
  static member hor(a: HPre.HFalse, b: HPre.HTrue) = HPre.HOr.hor(a, b)
  static member hor(a: HPre.HTrue, b: HPre.HTrue) = HPre.HOr.hor(a, b)

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix); RequireQualifiedAccess>]
module HBool =

  let htrue = HPre.Htrue
  let hfalse = HPre.Hfalse

type HCond =
  static member hcond(t: HPre.HFalse) = fun x y -> HPre.HCond.hcond(t, x, y)
  static member hcond(t: HPre.HTrue) = fun x y -> HPre.HCond.hcond(t, x, y)

type HNat =
  static member hsucc(n) = HPre.HNat.hsucc(n)
  static member hpred(n) = HPre.HNat.hpred(n)

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix); RequireQualifiedAccess>]
module HNat =

  let hzero = HPre.HNat.hzero()

  let inline toInt (a: HPre.HNat<_>) = a.ToInt()

type HEq =
  static member eq(a: HPre.HZero, b: HPre.HZero) = HPre.HEq.eq(a, b)
  static member eq(a: HPre.HZero, b: HPre.HSucc<_>) = HPre.HEq.eq(a, b)
  static member eq(a: HPre.HSucc<_>, b: HPre.HZero) = HPre.HEq.eq(a, b)
  static member eq(a, b, e) = HPre.HEq.eq(a, b, e)

type HAdd =
  static member add(a: HPre.HZero, b: HPre.HSucc<_>) = HPre.HAdd.add(a, b)
  static member add(a: HPre.HSucc<_>, b: HPre.HZero) = HPre.HAdd.add(a, b)
  static member add(a, b, h) = HPre.HAdd.add(a, b, h)
