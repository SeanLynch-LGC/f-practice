namespace StudentScores

module SchoolCodes =

    open System.IO
    open System.Collections.Generic
    let load (schoolCodesFilePath : string) =
        File.ReadAllLines schoolCodesFilePath
            |> Seq.skip 1
            |> Seq.map (fun row ->
                let elements = row.Split("\t")
                let id = elements.[0] |> int
                let name = elements.[1]
                (id,name))
            |> Map.ofSeq
            |> Map.add 0 "External"
            //|> dict
            
        // let pairs =
        //     File.ReadAllLines schoolCodesFilePath
        //     |> Seq.skip 1
        //     |> Seq.map (fun row ->
        //         let elements = row.Split("\t")
        //         let id = elements.[0] |> int
        //         let name = elements.[1]
        //         KeyValuePair.Create(id,name))
        // new Dictionary<int, string>(pairs)

