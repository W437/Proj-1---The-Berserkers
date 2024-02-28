// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class DwarfArcher : RangedUnit
{
    public DwarfArcher() : base(damage: new Dice(2, 7, 3), hp: 50, armor: 50, range: 300)
    {
        UnitRace = Race.Dwarf;
        CritChance = 0.20f;
        CritMultiplier = 2.0f;
        EquippedWeapon = new ArrowProjectile();
        CarryCapacity = 70;
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

}