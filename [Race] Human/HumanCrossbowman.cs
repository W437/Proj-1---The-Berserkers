// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanCrossbowman : RangedUnit
{
    public HumanCrossbowman(IRandomProvider randomProvider) : base(new Dice(2, 6, 3), new Dice(1, 5, 0), new Dice(1, 10, 0), hp: 65, armor: 5, range: 400)
    {
        SetCritChance(0.15f);
        SetCritMultiplier(1.2f);
        SetCarryCapacity(70);
        SetEvasionChance(0.15f);
        EquippedWeapon = new BoltProjectile();
    }
}

