// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanCrossbowman : RangedUnit
{

    // to do Make at least one unit associated with the Bag random.
    public HumanCrossbowman(IRandomProvider randomProvider) : base(randomProvider, hp: 65, armor: 5, range: 400)
    {
        SetCritChance(0.15f);
        SetCritMultiplier(1.2f);
        SetCarryCapacity(70);
        EquippedWeapon = new BoltProjectile();
    }
}
