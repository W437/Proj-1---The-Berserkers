// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanRogue : AssassinUnit
{
    public HumanRogue(IRandomProvider randomProvider) : base(randomProvider, hp: 70, armor: 10)
    {
        UnitRace = Race.Human;
        SetCritChance(0.10f);
        SetCritMultiplier(1.6f);
        SetCarryCapacity(50);
        EquippedWeapon = new VenomStinger();
    }
}
