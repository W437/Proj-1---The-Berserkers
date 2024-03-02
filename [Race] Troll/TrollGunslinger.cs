// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollGunslinger : AssassinUnit
{
    public TrollGunslinger() : base(new Dice(2, 7, 3), new Dice(1, 5, 0), new Dice(1, 10, 0), hp: 80, armor: 5)
    {
        UnitRace = Race.Troll;
        SetCritChance(0.15f);
        SetCritMultiplier(1.1f);
        SetCarryCapacity(70);
        SetEvasionChance(0.3f);
        EquippedWeapon = new Shadowblade();
    }
}
