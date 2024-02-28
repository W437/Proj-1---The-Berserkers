// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class DwarfBerserker : AssassinUnit
{
    public DwarfBerserker() : base(damage: new Dice(3, 7, 1), hp: 50, armor: 15)
    {
        UnitRace = Race.Dwarf;
        SetCritChance(0.15f);
        SetCritMultiplier(1.2f);
        SetCarryCapacity(70);
        SetEvasionChance(0.15f);
        EquippedWeapon = new Dagger();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

}
