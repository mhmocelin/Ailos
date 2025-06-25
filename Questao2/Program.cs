using Newtonsoft.Json;
using Questao2;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;        
        int totalPages = 1;
        var teamKey = new Dictionary<string, Func<Match, int>>
        {
            { "team1", match => match.Team1Goals },
            { "team2", match => match.Team2Goals }
        };

        foreach (var key in teamKey)
        {
            int page = 1;

            do
            {
                FootballMatch footballMatches = GetMatches(team, year, page, key.Key);

                totalPages = footballMatches.TotalPages;
                totalGoals += footballMatches.matches.Sum(teamKey[key.Key]);
                page++;
            }
            while (page <= totalPages);
        }

        return totalGoals;
    }

    public static FootballMatch GetMatches(string team, int year, int page, string teamKey)
    {
        string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamKey}={team}&page={page}";

        using (HttpClient client = new HttpClient())
        {
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<FootballMatch>(response.Content.ReadAsStringAsync().Result);
        }
    }
}