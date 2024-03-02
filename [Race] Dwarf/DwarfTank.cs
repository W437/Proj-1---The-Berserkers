// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class DwarfTank : RangedUnit
{
    public DwarfTank() : base(new Dice(2, 6, 3), new Dice(1, 5, 0), new Dice(1, 10, 0), hp: 80, armor: 10, range: 200)
    {
        UnitRace = Race.Dwarf;
        SetCritChance(0.10f);
        SetCritMultiplier(1.25f);
        SetCarryCapacity(90);
        SetEvasionChance(0.15f);
        EquippedWeapon = new BoltProjectile();
    }
}
