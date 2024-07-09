//https://exercism.org/tracks/fsharp/exercises/yacht
module Yacht

type Category = 
    | Ones
    | Twos
    | Threes
    | Fours
    | Fives
    | Sixes
    | FullHouse
    | FourOfAKind
    | LittleStraight
    | BigStraight
    | Choice
    | Yacht

type Die =
    | One
    | Two 
    | Three
    | Four 
    | Five 
    | Six

let MatchPlantToValue x  = [|
    for i in x do
        match i with
        | One -> 1
        | Two -> 2
        | Three -> 3
        | Four -> 4
        | Five -> 5
        | Six -> 6
    |]

let valueMap = Map [ One, 1; Two, 2; Three, 3; Four, 4; Five, 5; Six, 6]

let TotalOfType die x  = [|
    for i in x do
        if valueMap[die] = i then
            i
    |]

let MatchOneType category dice =
    let die = match category with
                    | Ones -> One
                    | Twos -> Two
                    | Threes -> Three
                    | Fours -> Four
                    | Fives -> Five
                    | Sixes -> Six
                    | _ -> One
    dice |> MatchPlantToValue |> Array.sort |> TotalOfType die |> Array.sum

let yacht dice : int =
    let sum = dice |> MatchPlantToValue |> Array.sum
    let testSum = pown (dice |> MatchPlantToValue |> Seq.item 0) 2
    if sum = testSum then
        50
    else
        0

let checkOrderBig (dice: int array) =
    if (dice.[0]+1 = dice.[1] && dice.[2]+1 = dice.[3] && dice.[3]+1 = dice.[4] && dice.[4] = 6) then
        30
    else
        0

let checkOrderLittle (dice: int array) =
    if (dice.[0]+1 = dice.[1] && dice.[2]+1 = dice.[3] && dice.[3]+1 = dice.[4] && dice.[4] = 5) then
        30
    else
        0

let BigStraight dice =
    dice |> MatchPlantToValue |> Array.sort |> checkOrderBig

let LittleStraight dice =
    dice |> MatchPlantToValue |> Array.sort |> checkOrderLittle

let check4Kind (dice:array<int>) =
    if (dice.[0] = dice.[1] && dice.[2] = dice.[3] && dice.[2] = dice.[1]) then
        dice.[3] * 4
    elif (dice.[4] = dice.[1] && dice.[2] = dice.[3] && dice.[2] = dice.[1]) then
        dice.[1] * 4
    else
        0

let checkfullHouse (dice:array<int>) =
    if (dice.[0] = dice.[1] && dice.[4] = dice.[3] && dice.[2] = dice.[1] && dice.[4] <> dice.[0]) then
        (dice.[1] * 3) + (dice.[3] * 2)
    elif (dice.[1] = dice.[0] && dice.[2] = dice.[3] && dice.[2] = dice.[4] && dice.[4] <> dice.[0]) then
        (dice.[3] * 3) + (dice.[1] * 2)
    else
        0
    
let matchFour dice =
    dice |> MatchPlantToValue |> Array.sort |> check4Kind

let fullhouse dice =
    dice |> MatchPlantToValue |> Array.sort |> checkfullHouse

let score category (dice : list<Die>) =
    match category with
    | Yacht -> yacht dice
    | Choice -> dice |> MatchPlantToValue |> Array.sum
    | BigStraight -> BigStraight dice
    | LittleStraight -> LittleStraight dice
    | FourOfAKind -> matchFour dice
    | FullHouse -> fullhouse dice
    | Sixes -> MatchOneType category dice
    | Fives -> MatchOneType category dice
    | Fours -> MatchOneType category dice
    | Threes -> MatchOneType category dice
    | Twos -> MatchOneType category dice
    | Ones -> MatchOneType category dice