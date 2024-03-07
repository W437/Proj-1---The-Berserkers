// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public abstract class AssassinUnit : Unit
{
    public virtual bool IsStealth { get; set; }

    protected AssassinUnit(IRandomProvider randomProvider, int hp, int armor)
        : base(randomProvider, hp, armor)
    {
        IsStealth = true;
    }
}
