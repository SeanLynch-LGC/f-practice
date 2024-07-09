// For more information see https://aka.ms/fsharp-console-a
let mutable count = 1.0
let loop = 
    [| for i in 0..20 -> (
        if i % 2 = 1 then
            count <- count + (count/2.0)
            count
        else
            count <- count - (count/2.0)
            count) |]

printfn "%A" loop

loop.[9] |> ( fun x -> 
                count <- x+ x
                for i in 0..9 do
                    count <- count + count
                    
                count) 
        |> printfn "%A"