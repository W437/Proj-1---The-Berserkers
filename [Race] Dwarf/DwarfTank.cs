// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class DwarfTank : RangedUnit
{
    public DwarfTank() : base(damage: new Dice(2, 6, 3), hp: 70, armor: 10, range: 200)
    {
        UnitRace = Race.Dwarf;
        SetCritChance(0.10f);
        SetCritChance(1.5f);
        EquippedWeapon = new BoltProjectile();
        SetCarryCapacity(90);
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

}