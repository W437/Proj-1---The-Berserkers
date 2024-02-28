// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollScout : AssassinUnit
{
    public TrollScout() : base(damage: new Dice(2, 7, 3), hp: 40, armor: 80)
    {
        UnitRace = Race.Troll;
        CritChance = 0.40f;
        CritMultiplier = 1.4f;
        EvasionChance += 0.25f;
        CarryCapacity = 50;
        EquippedWeapon = new TrollStriker();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }
}
