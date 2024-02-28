// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class DwarfBerserker : AssassinUnit
{
    public DwarfBerserker() : base(damage: new Dice(3, 7, 1), hp: 50, armor: 30)
    {
        UnitRace = Race.Dwarf;
        CritChance = 0.35f;
        CritMultiplier = 1.7f;
        EvasionChance += 0.08f;
        CarryCapacity = 70;
        EquippedWeapon = new Dagger();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

}
