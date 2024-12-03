open System.Text.RegularExpressions
type Mode =
    | Process
    | Skip

let regex = Regex @"(mul\((\d+),(\d+)\))|(do\(\))|(don't\(\))"
let input = System.IO.File.ReadAllText "input.txt"

let determineMode (currentMatch: Match) currentMode =
    match currentMatch.Value with
    | "do()" -> Process
    | "don't()" -> Skip
    | _ -> currentMode

let ProcessMatch (currentMode, runningTotal) currentMatch =
    let updatedMode = determineMode currentMatch currentMode
    match updatedMode with
    | Process when currentMatch.Value.StartsWith "mul" -> Process, runningTotal + int currentMatch.Groups[2].Value * int currentMatch.Groups[3].Value 
    | _ -> updatedMode, runningTotal

let matches = regex.Matches input

let result =
    matches
    |> Seq.fold ProcessMatch (Process, 0)
    |> snd
    |> printfn "%d"
