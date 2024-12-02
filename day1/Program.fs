open System.IO

let filePath = "input.txt"
let fileContent = File.ReadAllLines(filePath)

let parseLine (line: string)=
    let parts = line.Split(' ') |> Array.filter (fun s -> s <> "")
    let firstValue = parts[0].Trim() |> int
    let secondValue = parts[1].Trim() |> int
    firstValue, secondValue
       
let sortArrays (firstArray, secondArray) =
    Array.sort firstArray, Array.sort secondArray
    
let calcDistances (firstSortedArray, secondSortedArray) =
    Array.map2 (fun x y -> abs (x - y)) firstSortedArray secondSortedArray
    |> Array.sum
    
let occurrences value array =
    Array.filter ((=) value) array |> Array.length
    
let calcSimilarities (firstSortedArray, secondSortedArray) =
    Array.sumBy (fun x -> x * occurrences x secondSortedArray) firstSortedArray
                  
    
let data = 
    fileContent 
    |> Array.map parseLine
    |> Array.unzip
    |> sortArrays
    |> calcSimilarities // Swap in calcDistances if solving part 1
    |> printfn "%d"


