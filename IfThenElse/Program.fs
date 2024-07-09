module CarsAssemble

let successRate (speed: int): float =
    if speed = 0 then
        0
    elif speed > 0 && speed < 5 then
        1
    elif speed > 4 && speed < 9 then
        0.9
    elif speed = 9 then
        0.8
    else
        0.77

let productionRatePerHour (speed: int): float =
    let x = float(speed) * 221.0
    let success = successRate speed
    x * success

let workingItemsPerMinute (speed: int): int =
    let x  = int((productionRatePerHour 6) / 60.0)
    x

type Die =
    | One
    | Two 
    | Three
    | Four 
    | Five 
    | Six

[<EntryPoint>]
let main args =
    // let x = KindergartenGarden.plants "RC\nGG" "Alice"
    // printfn "%A" x
    // let x = [|One;One;Three;Two;One|]
    // let valueMap = Map [ One, 1; Two, 2; Three, 3; Four, 4; Five, 5; Six, 6]
    // x |> MatchPlantToValue |> Array.sort |> printfn "%A"
    
    //printfn "%A" <| PhoneNumber.clean "523-@:!-7890"
    // printfn "%A" Poker.test
    // printfn "%A" Poker.test2

   // printfn "%A" (Poker.BestHands [| "3D 4S 5S 6S 7S"; "2S 4S 5S 6S 7S";"3S 4S 5S 6S 7S"|])

    // printfn "%A" (Testfile.convertString "of lung")
    // let rx = ref "I'm rx"
    // let ry = rx
    // ry := "I'm ry"
    // printfn ""
    // printfn "%s|%s" rx.Value ry.Value
    0