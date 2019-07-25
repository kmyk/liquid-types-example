module Liquid.Program

open FSharp.Text.Lexing
open Liquid.Syntax
open System

let newGensym() : unit -> string =
    let i = ref 0
    fun () ->
        i := !i + 1
        sprintf "%d" !i

[<EntryPoint>]
let main argv =
    let lexbuf = LexBuffer<char>.FromTextReader Console.In

    let parsed =
        try
            Parser.start Lexer.tokenstream lexbuf
        with e when e.Message.Equals "parse error" -> raise (Exception(sprintf "SyntaxError: Unexpected token: \"%s\" Line: %d Column: %d" (LexBuffer<_>.LexemeString lexbuf) (lexbuf.StartPos.Line + 1) (lexbuf.StartPos.Column + 1)))

    let gensym = newGensym()
    let parsed = parsed gensym
    printfn "%A" parsed
    0
