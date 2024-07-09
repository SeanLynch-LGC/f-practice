let inline (|>) arg func = func arg
let inline (>>) func1 func2 x = func2 (func1 x)

let add x y = x + y
let add10 = add 10

let cities _ =
    let citiesDict =
        [ ("Dublin", "Something nice about Dublin")
          ("Cork", "Something nice about Cork (is there anything nice ?)") ]
        |> Map.ofSeq

    printfn "Initial part executed"

    fun city ->
        printfn "Lazy part executed"
        citiesDict.[city]

open System

type CustomerId = CustomerId of Guid
type OrderId = OrderId of Guid

let payOrder (CustomerId customerId) (OrderId orderId) =
    printfn "Order %O from Customer %O is now paid." orderId customerId
    ()

let customerId = Guid.NewGuid() |> CustomerId
let orderId = Guid.NewGuid() |> OrderId

payOrder customerId orderId

type City = { Id: int; Name: string }
type Country = { Id: int; Name: string }

let dublin = { City.Id = 1; Name = "Dublin" }

let doSomething x =
    match x with
    | Some x'''''''''' -> x''''''''''
    | None -> failwith "No value"

let myList = [ 1..10 ]

let double x =
    printfn "will double x: %i and become %i" x (x * 2)
    x * 2

let square x =
    printfn "will square x: %i and become %i " x (x * x)
    x * x

myList |> List.map double |> List.map square

myList
|> Seq.ofList
|> Seq.map double
|> Seq.map square
|> List.ofSeq

myList |> List.map (double >> square)

let firstname = Some "Paulo"
let lastname = Some "Nobre"
let middle = None

let id' x = x
let konst x _ = x

let join (sep: char) (xs: List<_>) = System.String.Join(sep, Seq.ofList xs)

[ firstname; middle; lastname ]
|> List.choose id'
|> join ' '


[ 1; 2; 2; 1; 3; 3; 6 ] |> Set.ofList

type UserRoles =
    | Nurse
    | Physician
    | Pharmacist
    | RegisteredNurse

let nurse = RegisteredNurse

match nurse with
| Nurse
| RegisteredNurse -> printfn "access granted"
| Physician
| Pharmacist -> printfn "access denied"


let someList = [ 1; 2; 3; 4; 5 ]

match someList with
| [] -> printfn "empty list"
| [ first ] -> printfn "single element: %i" first
| first :: rest -> printfn "one or more elements. head is %i, tail is %A" first rest

let someArray = [| 1; 2; 3 |]

match someArray |> Array.truncate 2 with
| [| singleton |] -> printfn "single element"
| [| first; second |] -> printfn "two elements"
| _ -> printfn "I don't care"

let someInt = 1

match someInt with
| 0 -> printfn "it's a 0"
| 1 -> printfn "it's a 1 "


open System

type Customer = { Id: Guid; Name: string }

// OptionalAsyncResult
type OAR<'a> = Async<Result<Option<'a>, exn>>

let getCustomer (id: Guid) : OAR<Customer> =
    async {
        return Ok <| Some { Id = id; Name = "Paulo" }
    //return Ok None
    //return Error <| exn "Fuck it"
    }

let doSomething (customer: Customer) : OAR<Customer> = async { return Some customer |> Ok }

let updateCustomer (customer: Customer) : OAR<Customer> = async { return Some customer |> Ok }

let id = Guid.NewGuid()

let maybeCustomerRes = getCustomer id |> Async.RunSynchronously

let maybeDoSomething =
    match maybeCustomerRes with
    | Error e -> Error e |> async.Return
    | Ok maybeCustomer ->
        match maybeCustomer with
        | None -> None |> Ok |> async.Return
        | Some customer ->
            let updateCustomerRes = doSomething customer |> Async.RunSynchronously

            match updateCustomerRes with
            | Error e -> Error e |> async.Return
            | Ok c -> c |> Ok |> async.Return
    |> Async.RunSynchronously

match maybeDoSomething with
| Error _ -> printfn "error retrieving customer"
| Ok maybeCustomer ->
    match maybeCustomer with
    | None -> printfn "No customer"
    | Some customer ->
        let updateCustomerRes = updateCustomer customer |> Async.RunSynchronously

        match updateCustomerRes with
        | Error _ -> printfn "error updating customer"
        | Ok c -> printfn "customer %O | %s" customer.Id customer.Name

// map: (a' -> 'b) -> M<'a> -> M<'b>
// bind: (a' -> M<'b>) -> M<'a> -> M<'b>

[<RequireQualifiedAccessAttribute>]
module OAR =
    let retn (x: 'a) : OAR<'a> = async { return Some x |> Ok }

    let bind (f: 'a -> OAR<'b>) (a: OAR<'a>) : OAR<'b> =
        async {
            match! a with
            | Error e -> return Error e
            | Ok x ->
                match x with
                | None -> return Ok None
                | Some x' -> return! f x'
        }

    let map (f: 'a -> 'b) (a: OAR<'a>) : OAR<'b> = bind (f >> retn) a

    module Operators =
        let (>=>) (f: 'a -> OAR<'b>) (g: 'b -> OAR<'c>) : ('a -> OAR<'c>) = fun a -> bind g (f a)

        let (=) a b =
            async {
                let! a' = a
                let! b' = b
                return a' = b'
            }
            |> Async.RunSynchronously


getCustomer id
|> OAR.bind doSomething
|> OAR.bind updateCustomer
|> Async.RunSynchronously

open OAR.Operators

let crap = getCustomer >=> doSomething >=> updateCustomer

crap id |> Async.RunSynchronously

getCustomer id
|> OAR.map (fun c -> { c with Name = "Nobre" })
|> OAR.bind doSomething
|> OAR.bind updateCustomer
|> Async.RunSynchronously

getCustomer id
|> OAR.map (fun c -> { c with Name = "Nobre" })
|> OAR.bind (doSomething >=> updateCustomer)
|> Async.RunSynchronously


// ----- Monad Laws verification
let b = getCustomer
let b' = b id |> Async.RunSynchronously

// 1. Right Identity
let r = getCustomer >=> OAR.retn
let r' = r id |> Async.RunSynchronously

// 2. Left Identity
let l = OAR.retn >=> getCustomer

let l' = l id |> Async.RunSynchronously

// 3. Associativity
let a' = (getCustomer >=> doSomething) >=> updateCustomer
let a'' = getCustomer >=> (doSomething >=> updateCustomer)

a' id |> Async.RunSynchronously
a'' id |> Async.RunSynchronously


// ---- Applicatives

let addOne x = x + 1
let addOneOpt = Option.map addOne

addOneOpt (Some 1)

let add x y = x + y // int -> int -> int
let addOpt = Option.map add // (Option<int> -> Option<int -> int>)
addOpt (Some 1) // Option<(int -> int)>
addOpt (Some 1) (Some 2)

Option.map2 add (Some 1) (Some 2)

let add3 x y z = x + y + z
Option.map3 add3 (Some 1) (Some 2) (Some 3)

let add5 x y z h j = x + y + z + h + j

module Option =
    let retn a = Some a

    let apply (f: Option<'a -> 'b>) (a: Option<'a>) : Option<'b> =
        match f, a with
        | Some f', Some a' -> f' a' |> Some
        | _ -> None

    let applyM (f: Option<'a -> 'b>) (a: Option<'a>) : Option<'b> =
        Option.bind (fun f' -> Option.map f' a) f

    let mapA f a = apply (retn f) a

    let mapM f a = Option.bind (f >> retn) a

    module Operators =
        let (<*>) = apply
        let (<!>) = Option.map

Option.apply (Option.apply (Option.apply (Option.apply (Option.map add5 (Some 1)) (Some 1)) (Some 1)) (Some 1)) (Some 1)

open Option.Operators

add5 <!> (Some 1)
<*> (Some 1)
<*> (Some 1)
<*> (Some 1)
<*> (Some 1)

(Some add5)
<*> (Some 1)
<*> (Some 1)
<*> (Some 1)
<*> (Some 1)
<*> (Some 1)

// Applicatives in Async
open System.Threading

type AsyncBuilder with
    member async.MergeSources(left: Async<'T>, right: Async<'S>) : Async<'T * 'S> =
        async {
            let box f =
                async {
                    let! x = f
                    return x :> obj
                }

            let! results = Async.Parallel [| box left; box right |]
            return (results.[0] :?> 'T, results.[1] :?> 'S)
        }

let comp1 _ =
    async {
        printfn "Comp1 in Thread %i"
        <| Thread.CurrentThread.ManagedThreadId

        do! Async.Sleep 10000
        return 1
    }

let comp2 _ =
    async {
        printfn "Comp2 in Thread %i"
        <| Thread.CurrentThread.ManagedThreadId

        do! Async.Sleep 10000
        return 2
    }

let comp3 _ =
    async {
        printfn "Comp3 in Thread %i"
        <| Thread.CurrentThread.ManagedThreadId

        do! Async.Sleep 10000
        return 3
    }

// applicative example (Task.WhenAll)
let compA =
    async {
        printfn "CompA in Thread %i"
        <| Thread.CurrentThread.ManagedThreadId

        let! c1 = comp1 () // These are independent from the result of the previous computations
        and! c2 = comp2 ()
        and! c3 = comp3 ()

        return (c1, c2, c3)
    }

compA |> Async.RunSynchronously

// monadic example (Task.ContinueWith)
let compM =
    async {
        printfn "CompM in Thread %i"
        <| Thread.CurrentThread.ManagedThreadId

        let! ca = compA
        let! c1 = comp1 ca // These depend the result of the previous computations
        let! c2 = comp2 c1
        let! c3 = comp3 c2

        return (ca, c1, c2, c3)
    }

compM |> Async.RunSynchronously