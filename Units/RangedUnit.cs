// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public abstract class RangedUnit : Unit
{
    public int Range { get; set; }
    protected RangedUnit(IRandomProvider randomProvider, int hp, int armor, int range)
        : base(randomProvider, hp, armor)
    {
        Range = range;
    }
}
