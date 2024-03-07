// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanSniper : AssassinUnit
{
    public HumanSniper(IRandomProvider randomProvider) : base(randomProvider, hp: 80, armor: 15)
    {
        UnitRace = Race.Human;
        SetCritChance(0.15f);
        SetCritMultiplier(1.2f);
        SetCarryCapacity(50);
        EquippedWeapon = new PhantomSniper();
    }
}
