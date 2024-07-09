module ValentinesDay
    
    type Approval = 
        | Yes
        | No
        | Maybe
    
    // TODO: please define the 'Cuisine' discriminated union type
    type Cuisine = 
        | Korean
        | Turkish
    
    // TODO: please define the 'Genre' discriminated union type
    type Genre = 
        | Crime
        | Horror
        | Romance
        | Thriller
    
    // TODO: please define the 'Activity' discriminated union type
    type Activity =
        | BoardGame
        | Chill
        | Movie of Genre
        | Restaurant of Cuisine
        | Walk of int

    let rateActivity (activity: Activity): Approval = 
        match activity with
        | BoardGame -> No
        | Chill -> No
        | Movie(genre) when genre = Romance -> Yes
        | Restaurant(cuisine) when cuisine = Korean -> Yes
        | Restaurant(cuisine)  when cuisine = Turkish -> Maybe
        | Walk(x) when x < 3 -> Yes
        | Walk(x) when x < 5 -> Maybe
        | _ -> No
        