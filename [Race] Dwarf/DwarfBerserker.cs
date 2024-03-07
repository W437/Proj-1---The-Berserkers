// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class DwarfBerserker : AssassinUnit
{
    public DwarfBerserker(IRandomProvider randomProvider) : base(randomProvider, hp: 50, armor: 15)
    {
        UnitRace = Race.Dwarf;
        SetCritChance(0.15f);
        SetCritMultiplier(1.2f);
        SetCarryCapacity(70);
        EquippedWeapon = new Dagger();
    }
}
