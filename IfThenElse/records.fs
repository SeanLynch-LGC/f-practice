module Bandwagoner

// TODO: please define the 'Coach' record type
type Coach = {
    Name : string
    FormerPlayer : bool
}

// TODO: please define the 'Stats' record type
type Stats = {
    Wins : int
    Losses : int
}

// TODO: please define the 'Team' record type
type Team = {
    Name : string
    Coach : Coach
    Stats : Stats
}

let createCoach (name: string) (formerPlayer: bool): Coach =
    let newCoach = { Name = name; FormerPlayer = formerPlayer}
    newCoach

let createStats(wins: int) (losses: int): Stats =
    let stats = { Wins=wins; Losses=losses}
    stats

let createTeam(name: string) (coach: Coach)(stats: Stats): Team =
    let createTeam = {Name=name; Coach=coach; Stats=stats}
    createTeam

let replaceCoach(team: Team) (coach: Coach): Team =
   {team with Coach=coach}

let isSameTeam(homeTeam: Team) (awayTeam: Team): bool =
    if homeTeam = awayTeam then
        true
    else
        false

let rootForTeam(team: Team): bool =
   match team with
    | team when team.Coach.Name = "Gregg Popovich" -> true
    | team when team.Coach.FormerPlayer = true -> true
    | team when team.Name = "Chicago Bulls" -> true
    | team when team.Stats.Wins > 59 -> true
    | team when team.Stats.Wins < team.Stats.Losses -> true
    | _ -> false
