type Trajectory =
    | Increasing
    | Decreasing

let allSameTrajectories tuples =
    let determineTrajectory (a, b) =
        if a > b then
            Decreasing
        else
            Increasing
    let trajectories = Array.map determineTrajectory tuples
    Array.forall (fun tuple -> determineTrajectory tuple = trajectories[0]) tuples

let allWithinRange tuples =
    let withinDesiredRange (a, b) =
        let inRange min max value =
            value >= min && value <= max
        let absoluteDifference = abs (a - b)
        inRange 1 3 absoluteDifference
    Array.forall (fun tuple -> withinDesiredRange tuple) tuples

let isSafe levels = 
    let pairs = Array.pairwise levels
    if allWithinRange pairs && allSameTrajectories pairs then
        true
    else
        false

// Part 2 - remove one element from the array and check if it's now safe
let canRemoveOneToMakeSafe (levels: int array) =
    let checkPermutation i _ =
        let newLevels = Array.removeAt i levels
        isSafe newLevels
    Array.mapi checkPermutation levels
    |> Array.contains true


let fileContents = System.IO.File.ReadAllText "input.txt"
fileContents.Split('\n', System.StringSplitOptions.RemoveEmptyEntries)
    |> Array.map (fun line -> line.Split(' ', System.StringSplitOptions.RemoveEmptyEntries))
    |> Array.map (fun values -> Array.map int values)
    |> Array.filter (fun levels -> isSafe levels || canRemoveOneToMakeSafe levels)
    |> Array.length
    |> printfn "%d"
