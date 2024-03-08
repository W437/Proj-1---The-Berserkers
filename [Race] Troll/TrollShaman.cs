// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollShaman : RangedUnit
{
    public TrollShaman(IRandomProvider randomProvider) : base(randomProvider, hp: 75, armor: 10, range: 250)
    {
        UnitRace = Race.Troll;
        SetCritChance(0.15f);
        SetCritMultiplier(1.3f);
        SetCarryCapacity(50);
        EquippedWeapon = new ShamanicEnergyProjectile();
    }
}
