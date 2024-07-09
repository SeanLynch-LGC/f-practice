open Anagram

// let candidates = ["hello"; "world"; "zombies"; "pants"]

// findAnagrams candidates "diaper" |> printfn "%A"

let target = "stone"
//let candidates = [| "stone"; "tones"; "banana"; "tons"; "notes"; "Seton" |]
let candidates = [|"cashregister"; "Carthorse"; "radishes"|]
let anagrams = [| "tones"; "notes"; "Seton" |]
printfn "%A" <| Anagram.findAnagrams "Orchestra" candidates

// let candidates1 = ["gallery"; "ballerina"; "regally"; "clergy"; "largely"; "leading"]

// findAnagrams candidates1 "allergy" |> printfn "%A"
// let candidates = ["lemons"; "cherry"; "melons"]
// findAnagrams candidates "solemn" |> should equal ["lemons"; "melons"]

// let ``Does not detect anagram subsets`` () =
//     let candidates = ["dog"; "goody"]
//     findAnagrams candidates "good" |> should be Empty

// let ``Detects anagram`` () =
//     let candidates = ["enlists"; "google"; "inlets"; "banana"]
//     findAnagrams candidates "listen" |> should equal ["inlets"]

// let ``Detects three anagrams`` () =
//     let candidates = ["gallery"; "ballerina"; "regally"; "clergy"; "largely"; "leading"]
//     findAnagrams candidates "allergy" |> should equal ["gallery"; "regally"; "largely"]

// let ``Detects multiple anagrams with different case`` () =
//     let candidates = ["Eons"; "ONES"]
//     findAnagrams candidates "nose" |> should equal ["Eons"; "ONES"]

// let ``Does not detect non-anagrams with identical checksum`` () =
//     let candidates = ["last"]
//     findAnagrams candidates "mass" |> should be Empty

// let ``Detects anagrams case-insensitively`` () =
//     let candidates = ["cashregister"; "Carthorse"; "radishes"]
//     findAnagrams candidates "Orchestra" |> should equal ["Carthorse"]