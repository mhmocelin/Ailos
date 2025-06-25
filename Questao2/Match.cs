﻿using Newtonsoft.Json;

namespace Questao2;

public class Match
{
    [JsonProperty("competition")]
    public string Competition { get; set; }

    [JsonProperty("year")]
    public int Year { get; set; }

    [JsonProperty("round")]
    public string Round { get; set; }

    [JsonProperty("team1")]
    public string Team1 { get; set; }

    [JsonProperty("team2")]
    public string Team2 { get; set; }

    [JsonProperty("team1goals")]
    public int Team1Goals { get; set; }

    [JsonProperty("team2goals")]
    public int Team2Goals { get; set; }
}
