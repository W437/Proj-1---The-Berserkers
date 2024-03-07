// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class DwarfArcher : RangedUnit
{
    public DwarfArcher(IRandomProvider randomProvider) : base(randomProvider, hp: 75, armor: 15, range: 300)
    {
        UnitRace = Race.Dwarf;
        SetCritChance(0.20f);
        SetCritMultiplier(1.1f);
        SetCarryCapacity(70);
        EquippedWeapon = new ArrowProjectile();
    }
}
