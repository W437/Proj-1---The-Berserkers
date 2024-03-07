// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollScout : AssassinUnit
{
    public TrollScout(IRandomProvider randomProvider) : base(randomProvider, hp: 65, armor: 20)
    {
        UnitRace = Race.Troll;
        SetCritChance(0.15f);
        SetCritMultiplier(1.35f);
        SetCarryCapacity(60);
        EquippedWeapon = new TrollStriker();
    }
}
