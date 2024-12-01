open System
open System.IO

let filePath = "input.txt"
let fileContent = File.ReadAllLines(filePath)

let parseLine (line: string)=
    let parts = line.Split(' ') |> Array.filter (fun s -> s <> "")
    let firstValue = parts.[0].Trim() |> int
    let secondValue = parts.[1].Trim() |> int
    firstValue, secondValue
    
    
let sortArrays (firstArray, secondArray) =
    let firstSortedArray = firstArray |> Array.sort
    let secondSortedArray = secondArray |> Array.sort
    (firstSortedArray, secondSortedArray)
    
let calcDistances arrays =
    let (firstSortedArray, secondSortedArray) = arrays
    firstSortedArray
    |> Array.map2 (fun x y -> abs (x - y)) secondSortedArray
    |> Array.sum
    
let data = 
    fileContent 
    |> Array.map parseLine
    |> Array.unzip
    |> sortArrays
    |> calcDistances
    |> printfn "%d"


