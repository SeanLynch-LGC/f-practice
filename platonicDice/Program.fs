// For more information see https://aka.ms/fsharp-console-apps
open platonicdice

[<EntryPoint>]
let main argv =
    let diceSizes = [|4;6;8;12;20|]
    let x = [| for i in 0..1000 -> dicegenerator.generateSum diceSizes|]
    printfn "%A" x
    x |> dicegenerator.getVariance |> printfn "%f"
    0