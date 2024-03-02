// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollScout : AssassinUnit
{
    public TrollScout() : base(new Dice(2, 7, 3), new Dice(1, 5, 0), new Dice(1, 10, 0), hp: 65, armor: 20)
    {
        UnitRace = Race.Troll;
        SetCritChance(0.15f);
        SetCritMultiplier(1.35f);
        SetCarryCapacity(60);
        SetEvasionChance(0.25f);
        EquippedWeapon = new TrollStriker();
    }
}
