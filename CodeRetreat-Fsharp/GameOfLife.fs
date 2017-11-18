module GameOfLife

open Swensen.Unquote
open Xunit

type NbLivingNeighbours = int

type CellState =
| Living
| Dead

let nextState nbLivingNeighbours currentCellState =
    Dead

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
