open System.Text.RegularExpressions

let regex = new Regex @"mul\((\d+),(\d+)\)"
let input = System.IO.File.ReadAllText "input.txt"

regex.Matches input 
    |> Seq.cast<Match>
    |> Seq.sumBy (fun m -> int m.Groups[1].Value * int m.Groups[2].Value)
    |> printfn "%d"
