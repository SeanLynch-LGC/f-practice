module Testfile

    let convertToRegex (char: char) : string =
        if char = ' ' then
            "."
        else
            "["+char.ToString().ToLower()+char.ToString().ToUpper()+"]"

    let prepend x y =
        Seq.append y x

    let convertString (text :string) : string =
        let newText: seq<string> = Seq.singleton ".*"
        let x = text.ToCharArray() 
                |> Seq.map convertToRegex 
                |> Seq.append newText 
                |> prepend [".*"]
                |> Seq.fold (fun (x: string) (a: string) -> x+a) ""
        x