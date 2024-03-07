// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollGunslinger : AssassinUnit
{
    public TrollGunslinger(IRandomProvider randomProvider) : base(randomProvider, hp: 80, armor: 5)
    {
        UnitRace = Race.Troll;
        SetCritChance(0.15f);
        SetCritMultiplier(1.1f);
        SetCarryCapacity(70);
        EquippedWeapon = new Shadowblade();
    }
}
