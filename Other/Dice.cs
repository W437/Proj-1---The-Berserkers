// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public struct Dice : IRandomProvider
{
    private uint DiceCount;
    private uint DiceSides;
    private int Modifier;

    public Dice(uint diceCount, uint diceSides, int modifier)
    {
        DiceCount = diceCount;
        DiceSides = diceSides;
        Modifier = modifier;
    }

    public int Next(int minValue, int maxValue)
    {
        return Random.Shared.Next(minValue, maxValue);
    }

    public double NextDouble()
    {
        return Random.Shared.NextDouble();
    }

    public int Roll(bool randModifier = false)
    {
        int result = 0;

        for (int i = 0; i < DiceCount; i++)
        {
            result += Next(1, (int)DiceSides + 1);
        }

        int randMod = Next(-5, 8);

        return randModifier ? (result + randMod) : result + Modifier;
    }

    public override string ToString()
    {
        return $"{DiceCount}d{DiceSides}{(Modifier >= 0 ? "+" : "")}{Modifier})";
    }

    public override bool Equals(object obj)
    {
        if (obj is Dice dice)
        {
            return dice.DiceSides == DiceSides && dice.Modifier == Modifier && dice.DiceCount == DiceCount;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (int)(DiceCount ^ DiceSides ^ Modifier);
    }

    public void ModifyModifier(int amount)
    {
        Modifier += amount;
    }

    public void SetModifier(int amount)
    {
        Modifier = amount;
    }

    public void ModifyScalar(int amount)
    {
        DiceCount = (uint)Math.Max(1, DiceCount + amount);
    }

    public int GetModifier()
    {
        return Modifier;
    }

}

/*class Program1
{
    static void Main()
    {
        Dice dicey = new Dice(1, 6, 2);
        Console.WriteLine("Roll: " + dicey.Roll());
        Console.WriteLine("String: " + dicey.ToString());
        Console.WriteLine("Hash: " + dicey.GetHashCode());
    }
}*/
