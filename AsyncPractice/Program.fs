open System
open System.IO

// Perform an asynchronous read of a file using 'async'
let printTotalFileBytesUsingAsync (path: string) =
    async {
        let! bytes = File.ReadAllBytesAsync(path) |> Async.AwaitTask
        let fileName = Path.GetFileName(path)
        printfn $"File {fileName} has %d{bytes.Length} bytes"
    }

[<EntryPoint>]
let main argv =
    printTotalFileBytesUsingAsync "lines.txt"
    |> Async.RunSynchronously

    Console.Read() |> ignore
    0