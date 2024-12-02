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

let fileContents = System.IO.File.ReadAllText "input.txt"
fileContents.Split('\n', System.StringSplitOptions.RemoveEmptyEntries)
    |> Array.map (fun line -> line.Split(' ', System.StringSplitOptions.RemoveEmptyEntries))
    |> Array.map (fun values -> Array.map int values)
    |> Array.filter isSafe
    |> Array.length
    |> printfn "%d"

//line.Split(' ') |> Array.filter (fun s -> s <> "")