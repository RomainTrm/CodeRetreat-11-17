module GameOfLife

open Swensen.Unquote
open Xunit

type NbLivingNeighbours = int

type CellState =
| Living
| Dead

let nextState nbLivingNeighbours currentCellState =
    Dead

[<Fact>]
let ``Should die when have less than two alive neighbours``() =
    test <@ Living |> nextState 1 = Dead @>


[<Fact>]
let ``Should die when have more than three alive neighbours``() =
    test <@ Living |> nextState 4 = Dead @>

