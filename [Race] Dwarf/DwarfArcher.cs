// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class DwarfArcher : RangedUnit
{
    public DwarfArcher() : base(damage: new Dice(2, 7, 3), hitChance: new Dice(1, 5, 0), defense: new Dice(1, 10, 0), hp: 75, armor: 15, range: 300)
    {
        UnitRace = Race.Dwarf;
        SetCritChance(0.20f);
        SetCritMultiplier(1.1f);
        SetCarryCapacity(70);
        EquippedWeapon = new ArrowProjectile();
    }
}
