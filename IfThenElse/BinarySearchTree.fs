module BinarySearchTree

type Node =
    struct 
        val parent : int
        val value : int
        val left : int
        val right : int
    end

let left (node : Node)  =
    node.left

let right (node : Node) =
    node.right

let data (node : Node) = 
    node.value

// let iterateThroughTree =


// let insertIntoTree startNode items =
//     Seq.removeAt 0 items
//     while true do
        // body-expression


// let create (items : array<int>) = 
//     let startNode = new Node (value = items.[0])
//     insertIntoTree startNode (items |> Seq.skip 1)

// let sortedData node = failwith "You need to implement this function."