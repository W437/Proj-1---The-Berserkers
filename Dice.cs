// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public struct Dice
{
    private uint Scalar;
    private uint BaseDie;
    private int Modifier;

    public Dice(uint scalar, uint baseDie, int modifier)
    {
        Scalar = scalar;
        BaseDie = baseDie;
        Modifier = modifier;
    }

    public int Roll(bool randModifier = false)
    {
        Random rand = new Random();
        int result = 0;

        for (int i = 0; i < Scalar; i++)
        {
            result += rand.Next(1, (int)BaseDie + 1);
        }

        int randMod = Random.Shared.Next(-5, 8);

        return randModifier ? (result + randMod) : result + Modifier;
    }

    public override string ToString()
    {
        return $"{Scalar}d{BaseDie}{(Modifier >= 0 ? "+" : "")}{Modifier})";
    }

    public override bool Equals(object obj)
    {
        if (obj is Dice dice)
        {
            return dice.BaseDie == BaseDie && dice.Modifier == Modifier && dice.Scalar == Scalar;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (int)(Scalar ^ BaseDie ^ Modifier);
    }

    public void ModifyModifier(int amount)
    {
        Modifier += amount;
    }

    public void ModifyScalar(int amount)
    {
        Scalar = (uint)Math.Max(1, Scalar + amount);
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
