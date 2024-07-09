//https://exercism.org/tracks/fsharp/exercises/phone-number
module PhoneNumber

let strip (stripChars: seq<char>) text =
    Seq.fold (fun (str: string) chr -> str.Replace(chr, ' ')) text stripChars

let checkX (number: string) (sequence: string) =
    (sequence + number)

let take number (list: char array) =
    let array = list |> Array.take number
    new string(array)

let checkInternationalCode (number :string) =
    let numbers: char array = Seq.toArray number
    if (number.[0] = '1') then
        numbers |> Array.skip 1
    else
        numbers

let toCharArray (number: string) =
    let numbers: char array = Seq.toArray number
    numbers

let checkForLetters(str) = 
    str
    |> Seq.map (fun c -> System.Char.IsLetter(c) || c = ' ')
    |> Seq.contains true

let checkForPunctuations(str) = 
    str
    |> Seq.map (fun c -> System.Char.IsPunctuation(c) || c = ' ')
    |> Seq.contains true

let getPhoneNumber (list: char array) =
    let seq = ""
    seq 
    |> checkX (list |> take 10)

let phone input =
    let list: char array = input |> strip ",./;'#[]\\|()+-." |> String.filter (fun x -> x <> ' ') |> checkInternationalCode
    let phoneNumber :string = getPhoneNumber list
    phoneNumber

let clean (input: string) = 
    let list: char array = input |> strip ",./;'#[]\\|()+-." |> String.filter (fun x -> x <> ' ') |> toCharArray
    let x : Result<uint64,string> =
        match input with
        | _ when list.Length < 10 -> Error "incorrect number of digits"
        | _ when list.Length = 11 && list.[0] <> '1' -> Error "11 digits must start with 1"
        | _ when list.Length > 11 -> Error "more than 11 digits"
        | _ when list.Length = 11 && list.[1] = '1' || list.Length = 10 && list.[0] = '1' -> Error "area code cannot start with one"
        | _ when list.Length = 11 && list.[1] = '0' || list.Length = 10 && list.[0] = '0' -> Error "area code cannot start with zero"
        | _ when list.Length = 11 && list.[4] = '1' || list.Length = 10 && list.[3] = '1' -> Error "exchange code cannot start with one"
        | _ when list.Length = 11 && list.[4] = '0' || list.Length = 10 && list.[3] = '0' -> Error "exchange code cannot start with zero"
        | _ when (checkForLetters list) -> Error "letters not permitted"
        | _ when (checkForPunctuations list) -> Error "punctuations not permitted"
        | _ -> Ok(uint64(phone input))
    x