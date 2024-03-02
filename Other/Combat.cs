// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
using System.Drawing;
using static Unit;

public class Combat
{
    private List<Unit>? team1;
    private List<Unit>? team2;
    private Random random = new Random();
    private int weatherDuration = 0;
    private bool isWeatherActive = false;
    private int combatRound = 0;

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

            Console.WriteLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($" [ ========================= ROUND {++combatRound} ========================= ] ");
            Console.ResetColor();
            Console.WriteLine();
            ApplyRandomWeather();

            Console.ResetColor();
            Console.WriteLine($"Contestants:");

            Console.WriteLine($" [+] {team1.Count(unit => unit.IsAlive())} alive (HP: {CalculateTeamHP(team1)}) ");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var unit in team1.Where(unit => unit.IsAlive()))
                PrintAliveUnitInfo(unit, "T1", ConsoleColor.Blue);
            Console.WriteLine($" [+] {team2.Count(unit => unit.IsAlive())} alive (HP: {CalculateTeamHP(team2)}) ");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var unit in team2.Where(unit => unit.IsAlive()))
                PrintAliveUnitInfo(unit, "T2", ConsoleColor.Red);

            Console.ForegroundColor = ConsoleColor.White;

            

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            // random units from each team to attack
            var attackerFromTeam1 = team1.Where(unit => unit.IsAlive())
                .OrderBy(x => random.Next())
                .FirstOrDefault();

            var targetFromTeam2 = team2.Where(unit => unit.IsAlive()).
                OrderBy(x => random.Next()).
                FirstOrDefault();

            var attackerFromTeam2 = team2.Where(unit => unit.IsAlive())
                .OrderBy(x => random.Next())
                .FirstOrDefault();

            var targetFromTeam1 = team1.Where(unit => unit.IsAlive())
                .OrderBy(x => random.Next())
                .FirstOrDefault();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n[ -A- ] {attackerFromTeam1.GetType().Name} ({attackerFromTeam1.HP} HP) attacks {targetFromTeam2.GetType().Name} ({targetFromTeam2.HP} HP)\n");
            Console.ForegroundColor = ConsoleColor.White;
            attackerFromTeam1?.Attack(targetFromTeam2);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ -A- ] {attackerFromTeam2.GetType().Name} ({attackerFromTeam2.HP} HP) attacks {targetFromTeam1.GetType().Name} ({targetFromTeam1.HP} HP)\n");
            Console.ForegroundColor = ConsoleColor.White;
            attackerFromTeam2?.Attack(targetFromTeam1);
        }

        // Announce winner & resources stolen
        var winner = team1.Any(unit => unit.IsAlive()) ? "Team 1" : "Team 2";
        int stolenResources = CalculateResourcesStolen(winner == "Team 1" ? team1 : team2);

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(@"
         **********************************************
         ************** BATTLE RESULTS ****************
         **********************************************");

        Console.ForegroundColor = winner == "Team 1" ? ConsoleColor.Blue : ConsoleColor.Red;
        Console.WriteLine($@"
              --- {winner.ToUpper()} EMERGES VICTORIOUS! ---
        ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($@"
               {winner} claims the spoils of war
                   seizing {stolenResources} resources!
        ");

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(@"
         **********************************************");
        Console.ForegroundColor = ConsoleColor.White;


    }

    private void PrintAliveUnitInfo(Unit unit, string teamTag, ConsoleColor unitColor)
    {
        Console.ForegroundColor = unitColor;
        Console.WriteLine($"[{teamTag}] {unit.GetType().Name}({unit.HP})");
        Console.ResetColor();
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
            Console.WriteLine($"***{currentEffect} weather has started and will last for {weatherDuration} turns.");

            switch (currentEffect)
            {
                case Weather.Sunny:
                    Console.WriteLine("*Sunny weather improves hit chance and range.");
                    break;

                case Weather.Cloudy:
                    Console.WriteLine("*Cloudy weather reduces evasion chance.");
                    break;

                case Weather.Rainy:
                    Console.WriteLine("*Rainy weather decreases defense rating.");
                    break;

                case Weather.Snowy:
                    Console.WriteLine("*Snowy weather significantly decreases defense rating.");
                    break;

                case Weather.Windy:
                    Console.WriteLine("*Windy weather reduces range.");
                    break;

                case Weather.Foggy:
                    Console.WriteLine("*Foggy weather greatly reduces range and increases evasion chance.");
                    break;

                default:
                    break;
            }

            Console.WriteLine("");
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
            new DwarfBerserker(),
            new HumanSniper(),
            new DwarfTank()
        };

        List<Unit> team2 = new List<Unit>
        {
            new DwarfArcher(),
            new TrollShaman(),
            new TrollScout()
        };

        Combat combat = new Combat();
        combat.StartCombat(team1, team2);
    }
}