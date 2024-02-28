// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollScout : AssassinUnit
{
    public TrollScout() : base(damage: new Dice(2, 7, 3), hp: 40, armor: 20)
    {
        UnitRace = Race.Troll;
        SetCritChance(0.15f);
        SetCritMultiplier(1.35f);
        SetCarryCapacity(60);
        SetEvasionChance(0.25f);
        EquippedWeapon = new TrollStriker();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }
}
