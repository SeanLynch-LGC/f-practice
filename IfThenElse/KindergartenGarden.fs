module KindergartenGarden

let findStudentNumber student :int =
    let students = [|"Alice";"Bob";"Charlie";"David";"Eve";"Fred";"Ginny";"Harriet";"Ileana";"Joseph";"Kincaid";"Larry"|]
    let index = Array.tryFindIndex (fun x -> x = student) students
    index.Value

    //use a map for students
let studentMap = ["Alice" , 0;"Bob", 1;"Charlie",2 ;"David",3;"Eve",4;"Fred",5;"Ginny",6;"Harriet",7;"Ileana",8;"Joseph",9;"Kincaid",10;"Larry",11]

type Plant =
    | Violets
    | Radishes
    | Clover
    | Grass

    with static member ofChar x =
            match x with
            | 'V' -> Some Violets
            | 'R' -> Some Radishes
            | 'C' -> Some Clover
            | 'G' -> Some Grass
            | _ -> None

let AddElementsToArray (studentNumber: int)  (diagram:string) =
    let split = diagram.Split("\n")
    let plant1 = split.[0].[studentNumber*2]
    let plant2 = split.[0].[studentNumber*2+1]
    let plant3 = split.[1].[studentNumber*2]
    let plant4 = split.[1].[studentNumber*2+1]
    [|plant1;plant2;plant3;plant4|]

let Match plant =
    match plant with
    | 'V' -> Violets
    | 'R' -> Radishes
    | 'C' -> Clover
    | 'G' -> Grass
    | _ -> Grass
    
let prependTo a b = Seq.append b a

// let CycleElements (diagram:char array) =
//     let mutable selection = Seq.empty<Plant>
//     for i = 0 to 3 do
//         let x = diagram.[i] |> Plant.ofChar |> Option.get |> Seq.singleton
//         selection <- Seq.append selection x
//     selection

let CycleElements (diagram:char array) = 
    Array.choose Plant.ofChar diagram

let plants diagram student =
    let studentNumber = findStudentNumber student
    diagram |> AddElementsToArray studentNumber
            |> CycleElements
            |> Seq.toList