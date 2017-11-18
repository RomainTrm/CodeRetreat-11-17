module GameOfLife

open Swensen.Unquote
open Xunit

type NbLivingNeighbours = int

type CellState =
| Living
| Dead

let nextState nbLivingNeighbours currentCellState =
    match currentCellState with
    | Living -> if nbLivingNeighbours = 2 || nbLivingNeighbours = 3
                then Living
                else Dead
    | Dead -> if nbLivingNeighbours = 3
              then Living
              else Dead

[<Theory>]
[<InlineData 0>]
[<InlineData 1>]
let ``Should die when have less than two alive neighbours`` nbLivingNeighbours =
    test <@ Living |> nextState nbLivingNeighbours = Dead @>

[<Theory>]
[<InlineData 4>]
[<InlineData 5>]
[<InlineData 6>]
[<InlineData 7>]
[<InlineData 8>]
let ``Should die when have more than three alive neighbours`` nbLivingNeighbours =
    test <@ Living |> nextState nbLivingNeighbours = Dead @>

[<Theory>]
[<InlineData 2>]
[<InlineData 3>]
let ``Should stay alive when have two or three alive neighbours`` nbLivingNeighbours =
    test <@ Living |> nextState nbLivingNeighbours = Living @>

[<Theory>]
[<InlineData 3>]
let ``Should become alive when have three alive neighbours`` nbLivingNeighbours =
    test <@ Dead |> nextState nbLivingNeighbours = Living @>

[<Theory>]
[<InlineData 2>]
let ``Should stay dead when have two alive neighbours`` nbLivingNeighbours =
    test <@ Dead |> nextState nbLivingNeighbours = Dead @>
