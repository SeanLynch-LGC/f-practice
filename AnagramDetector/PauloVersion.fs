module Anagram

    let findAnagrams (target: string) (candidates: string[]) = 
        let transform (x: string) = 
            x.ToCharArray() 
            |> Array.distinct
            |> Array.sort
            |> System.String
        let isAnagram a b = 
            a <> b && transform a = transform b 
        let candidatesLower = 
            candidates 
            |> Array.map (fun (x: string) -> x.ToLowerInvariant()) 
        candidates
        |> Array.mapi (fun idx c -> 
            if isAnagram (target.ToLowerInvariant()) candidatesLower[idx]
            then Some c
            else None)
        |> Array.choose id
    
    
