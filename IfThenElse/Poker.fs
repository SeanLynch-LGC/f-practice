module Poker

type Card =
| Ace
| King
| Queen
| Jack
| Ten
| Nine
| Eight
| Seven
| Six
| Five
| Four
| Three
| Two

type Suite =
| Diamonds
| Hearts
| Clubs
| Spades

type Hands =
| StraightFlush
| FourOfAKind
| FullHouse
| Flush 
| Straight 
| ThreeOfAKind 
| TwoPair 
| OnePair 
| HighCard 

let matchSuite (card: string) =
    match card with
    | "H" -> Hearts
    | "D" -> Diamonds
    | "C" -> Clubs
    | "S" -> Spades

let matchNumber (card: string) =
    let text = card.[0].ToString()
    match text with
    | "2" -> 2
    | "3" -> 3
    | "4" -> 4
    | "5" -> 5
    | "6" -> 6
    | "7" -> 7
    | "8" -> 8
    | "9" -> 9
    | "10" -> 10
    | "J" -> 11
    | "Q" -> 12
    | "K" -> 13
    | "A" -> 14

let removeFromString (card:string) =
    card.[0].ToString()

let flush (hand : string) : bool =
    let suites = hand.Split " " |>  Array.map (fun x -> x.[1].ToString()) |> Array.map (fun x -> matchSuite x)
    //let suites = Array.map matchSuite () |> Array.map (fun x -> removeFromString 1 x)
    Array.forall (fun (x : Suite) -> suites.[0] = x) suites
    
let straight (hand : string) : bool =
    let x  = hand.Split(" ") |> Array.map matchNumber |> Array.sort
    [|x.[0]..(x[0]+4)|] = x

let isFour grouping =
    grouping = 4

let fourofakind (hand: string) : bool =
    let x = ((hand.Split(" ")
    |> Array.map removeFromString
    |> Array.groupBy id 
    |> Array.filter (fun (x) -> (snd x).Length = 4) ))
    x.Length > 0

let threeofakind (hand: string) : bool =
    let x = hand.Split(" ")
            |> Array.map removeFromString
            |> Array.groupBy id 
            |> Array.filter (fun (x) -> (snd x).Length = 3)
    x.Length > 0

let twopair ( hand :string) :bool=
    ( hand.Split(" ") 
    |> Array.map removeFromString
    |> Array.groupBy id 
    |> Array.filter (fun (x) -> (snd x).Length <> 2) ).Length = 2

let onepair ( hand :string) :bool=
    ( hand.Split(" ") 
    |> Array.map removeFromString
    |> Array.groupBy id 
    |> Array.filter (fun (x) -> (snd x).Length <> 2) ).Length = 1

let fullhouse hand = 
    threeofakind hand && onepair hand

let highcard (hand:string) =
    let x = hand.Split(" ") |> Array.map matchNumber
    x.[4]

let removeNonWinningHands (hands: (Hands* string array) array) =
    let winningHand = fst hands.[0]
    hands 
    |> Array.map (fun x -> winningHand = fst x)
let highcardValue hand =
    hand

let commonHighCardValue hand =
    hand

let matchHandRanking hand =
    match hand with
    | _ when straight hand && flush hand -> (StraightFlush, hand, commonHighCardValue hand)
    | _ when fourofakind hand -> (FourOfAKind, hand, commonHighCardValue hand)
    | _ when fullhouse hand -> (FullHouse, hand, commonHighCardValue hand)
    | _ when flush hand -> (Flush, hand, commonHighCardValue hand)
    | _ when straight hand -> (Straight, hand, commonHighCardValue hand)
    | _ when threeofakind hand -> (ThreeOfAKind, hand, commonHighCardValue hand)
    | _ when twopair hand -> (TwoPair, hand, commonHighCardValue hand)
    | _ when onepair hand -> (OnePair, hand, commonHighCardValue hand)
    | _ -> (HighCard, hand, commonHighCardValue hand)

let calculateValueHand hand =
    match hand with
    | _ when straight hand || flush hand -> highcardValue hand
    | _ when fourofakind hand -> commonHighCardValue hand
    | _ when fullhouse hand -> commonHighCardValue hand
    | _ when threeofakind hand -> commonHighCardValue hand
    | _ when twopair hand -> commonHighCardValue hand
    | _ when onepair hand -> commonHighCardValue hand
    | _ -> highcardValue hand

let CompareHandValue (hand : Hands * string array) =
    calculateValueHand (snd hand).[0]

let BestHands (hands : string array) =
    hands 
    |> Array.map matchHandRanking 
    |> Array.sortBy (fun x -> fst x) 
   // |> removeNonWinningHands
   // |> CompareHandValue