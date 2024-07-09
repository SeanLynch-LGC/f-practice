module Anagram2

    let compareStrings (target: string) (test:string) =
        let targetCharArray = target.ToLower().ToCharArray()
        let testArray = test.ToLower().ToCharArray()
        let numOfElementsTarget = targetCharArray |> Array.countBy id |> Array.sortByDescending (fun x ->  (fst x) )
        let numOfElementsTest = testArray |> Array.countBy id  |> Array.sortByDescending (fun x ->  (fst x) )
        if targetCharArray.Length <> testArray.Length then
            None
        elif numOfElementsTarget.Length <> numOfElementsTest.Length then
            None
        elif target.ToLower() = test.ToLower() then
            None
        else
            let mutable count = 0
            for i in 0..numOfElementsTarget.Length-1 do
                if numOfElementsTarget.[i] = numOfElementsTest.[i] then
                    count <- count + 1
            if count = numOfElementsTarget.Length then
                Some test
            else None
            

    let findAnagrams sources target =
        sources |> List.choose (compareStrings target)