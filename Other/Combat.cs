// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
using static Unit;

public class Combat
{
    private List<Unit>? team1;
    private List<Unit>? team2;
    private Random random = new Random();
    private int weatherDuration = 0;
    private bool isWeatherActive = false;

    public void StartCombat(List<Unit> team1, List<Unit> team2)
    {
        this.team1 = team1;
        this.team2 = team2;

        // loop til one team has alive units
        while (team1.Any(unit => unit.IsAlive()) && team2.Any(unit => unit.IsAlive()))
        {
            Console.ReadLine();

            // weather chk
            if (isWeatherActive && weatherDuration > 0)
            {
                weatherDuration--;
            }
            else
            {
                isWeatherActive = false;
            }

            ApplyRandomWeather();

            // Print current state of combat
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"Current State: Team1: {team1.Count(unit => unit.IsAlive())} alive (total HP: {CalculateTeamHP(team1)}), Team2: {team2.Count(unit => unit.IsAlive())} alive (total HP: {CalculateTeamHP(team2)})");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            // random units from each team to attack
            var attackerFromTeam1 = team1.Where(unit => unit.IsAlive()).OrderBy(x => random.Next()).FirstOrDefault();
            var targetFromTeam2 = team2.Where(unit => unit.IsAlive()).OrderBy(x => random.Next()).FirstOrDefault();

            var attackerFromTeam2 = team2.Where(unit => unit.IsAlive()).OrderBy(x => random.Next()).FirstOrDefault();
            var targetFromTeam1 = team1.Where(unit => unit.IsAlive()).OrderBy(x => random.Next()).FirstOrDefault();

            // attack
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n--- {attackerFromTeam1} vs. {targetFromTeam2} ---\n");
            Console.ForegroundColor = ConsoleColor.White;
            attackerFromTeam1?.Attack(targetFromTeam2);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n--- {attackerFromTeam2} vs. {targetFromTeam1} ---\n");
            Console.ForegroundColor = ConsoleColor.White;
            attackerFromTeam2?.Attack(targetFromTeam1);
        }

        // Announce winner & resources stolen
        var winner = team1.Any(unit => unit.IsAlive()) ? "Team 1" : "Team 2";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n--- {winner} wins! ---");

        int stolenResources = CalculateResourcesStolen(winner == "Team 1" ? team1 : team2);
        Console.WriteLine($"\n --- {winner} has stolen {stolenResources} resources ---");

        Console.ForegroundColor = ConsoleColor.White;
    }

    private void ApplyRandomWeather()
    {
        if (!isWeatherActive && random.NextDouble() < 0.25) // 25% chance
        {
            // reset weather
            foreach (var unit in team1.Concat(team2))
            {
                unit.ResetWeatherEffects();
            }

            isWeatherActive = true;
            weatherDuration = random.Next(1, 5); // 1 to 4 steps
            Weather currentEffect = GetRandomWeather();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n **{currentEffect} weather has started and will last for {weatherDuration} turns.");
            Console.ForegroundColor = ConsoleColor.White;

            // apply to all units
            foreach (var unit in team1.Concat(team2))
            {
                unit.ApplyWeather(currentEffect);
            }
        }
    }


    private Weather GetRandomWeather()
    {
        var values = Enum.GetValues(typeof(Weather));
        return (Weather)values.GetValue(random.Next(values.Length));
    }


    private int CalculateResourcesStolen(List<Unit> winningTeam)
    {
        return winningTeam.Sum(unit => unit.CarryCapacity);
    }

    private int CalculateTeamHP(List<Unit> team)
    {
        return team.Sum(unit => unit.HP);
    }
}

public class Demo
{
    public static void Main(string[] args)
    {
        List<Unit> team1 = new List<Unit>
        {
            new DwarfArcher(),
            new TrollShaman(),
            new DwarfTank()
        };

        List<Unit> team2 = new List<Unit>
        {
            new DwarfBerserker(),
            new HumanSniper(),
            new TrollGunslinger()
        };

        Combat combat = new Combat();
        combat.StartCombat(team1, team2);
    }
}