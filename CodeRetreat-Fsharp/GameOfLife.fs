module GameOfLife

type CellState =
| Living
| Dead

let nextStateForLiving nbLivingNeighbours =
    match nbLivingNeighbours with
    | 2 -> Living
    | 3 -> Living
    | _ -> Dead

let nextStateForDead nbLivingNeighbours =
    match nbLivingNeighbours with
    | 3 -> Living
    | _ -> Dead

let nextState currentCellState =
    match currentCellState with
    | Living -> nextStateForLiving
    | Dead -> nextStateForDead


open Swensen.Unquote
open Xunit

[<Theory>]
[<InlineData 0>]
[<InlineData 1>]
let ``Should die when have less than two alive neighbours`` nbLivingNeighbours =
    test <@ nextState Living nbLivingNeighbours = Dead @>

[<Theory>]
[<InlineData 4>]
[<InlineData 5>]
[<InlineData 6>]
[<InlineData 7>]
[<InlineData 8>]
let ``Should die when have more than three alive neighbours`` nbLivingNeighbours =
    test <@ nextState Living nbLivingNeighbours = Dead @>

[<Theory>]
[<InlineData 2>]
[<InlineData 3>]
let ``Should stay alive when have two or three alive neighbours`` nbLivingNeighbours =
    test <@ nextState Living nbLivingNeighbours = Living @>

[<Theory>]
[<InlineData 3>]
let ``Should become alive when have three alive neighbours`` nbLivingNeighbours =
    test <@ nextState Dead nbLivingNeighbours = Living @>

[<Theory>]
[<InlineData 0>]
[<InlineData 1>]
[<InlineData 2>]
[<InlineData 4>]
[<InlineData 5>]
[<InlineData 6>]
[<InlineData 7>]
[<InlineData 8>]
let ``Should stay dead when have two alive neighbours`` nbLivingNeighbours =
    test <@ nextState Dead nbLivingNeighbours = Dead @>
