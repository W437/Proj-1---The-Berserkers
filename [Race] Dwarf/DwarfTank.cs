// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class DwarfTank : RangedUnit
{
    public DwarfTank() : base(damage: new Dice(2, 6, 3), hp: 70, armor: 10, range: 200)
    {
        UnitRace = Race.Dwarf;
        SetCritChance(0.10f);
        SetCritMultiplier(1.25f);
        SetCarryCapacity(90);
        SetEvasionChance(0.15f);
        EquippedWeapon = new BoltProjectile();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

}