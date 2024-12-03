open System.Text.RegularExpressions
type Mode =
    | Process
    | Skip
let regex = Regex @"(mul\((\d+),(\d+)\))|(do\(\))|(don't\(\))"
let input = System.IO.File.ReadAllText "input.txt"

let determineMode (m: Match) currentMode =
    match m.Value with
    | "do()" -> Process
    | "don't()" -> Skip
    | _ -> currentMode

let Calc (mode, sum) m =
    match determineMode m mode with
    | Process when m.Value.StartsWith "mul" -> Process, sum + int m.Groups[2].Value * int m.Groups[3].Value 
    | newMode -> newMode, sum

let matches = regex.Matches input

let result =
    matches
    |> Seq.cast<Match>
    |> Seq.fold Calc (Process, 0)
    |> snd
    |> printfn "%d"
