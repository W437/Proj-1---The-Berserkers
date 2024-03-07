// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class DwarfTank : RangedUnit
{
    public DwarfTank(IRandomProvider randomProvider) : base(randomProvider, hp: 80, armor: 10, range: 200)
    {
        UnitRace = Race.Dwarf;
        SetCritChance(0.10f);
        SetCritMultiplier(1.25f);
        SetCarryCapacity(90);
        EquippedWeapon = new BoltProjectile();
    }
}
