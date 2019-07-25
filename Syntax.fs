module Liquid.Syntax

type TyName = TyName of string

type ValName = ValName of string

type AType =
    | BoolATy
    | IntATy

type RType<'qual> =
    | BaseRTy of ValName * AType * 'qual
    | FunRTy of ValName * RType<'qual> * RType<'qual>
    | VarRTy of TyName

type Schema<'typ> =
    | Monotype of 'typ
    | Polytype of TyName * Schema<'typ>

type IntExpr =
    | VarIExp of ValName
    | LiteralIExp of bigint
    | AddIExp of IntExpr * IntExpr
    | SubIExp of IntExpr * IntExpr
    | MulIExp of bigint * IntExpr
    | UninterpretedIExp of ValName * list<IntExpr>

type BoolExpr =
    | LiteralBExp of bool
    | EqBExp of IntExpr * IntExpr
    | LtBExp of IntExpr * IntExpr
    | AndBExp of BoolExpr * BoolExpr
    | OrBExp of BoolExpr * BoolExpr
    | NotBExp of BoolExpr

type BType = RType<unit>

type Type = RType<BoolExpr>

type With<'t, 'info> =
    { value : 't
      info : 'info }

type Location =
    { line : int
      column : int }

type Literal =
    | BoolLit of bool
    | IntLit of bigint

type ExprWith<'info> = With<Expr<'info>, 'info>

and Expr<'info> =
    | VarExp of ValName
    | LiteralExp of Literal
    | LamExp of ValName * ExprWith<'info>
    | AppExp of ExprWith<'info> * ExprWith<'info>
    | IfThenElseExp of ExprWith<'info> * ExprWith<'info> * ExprWith<'info>
    | LetExp of ValName * ExprWith<'info> * ExprWith<'info>
    | LetRecExp of ValName * ValName * ExprWith<'info> * ExprWith<'info>
    | TyAbsExp of ValName * ExprWith<'info>
    | TyAppExp of ExprWith<'info> * Type
