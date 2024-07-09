namespace platonicdice

open System

module dicegenerator =

    let dice (upperLimit: int): int =
        let a () = (new Random()).Next(upperLimit)
        let r = a ()
        r
    
    let generateSum diceSizes: int =
        let mutable sum = 0
        for i in diceSizes do
            sum <- 0
            for x in 0..i do
                sum <- sum + i |> dice
        sum
        
    let getVariance (sums: int[]) : float =
        let populationMean = (sums |> Array.sum) / sums.Length
        let mutable sum = 0
        for i in 0.. sums.Length-1 do
            let top = sums.[i] - populationMean
            sum <- pown top 2
        float sum  / float sums.Length

