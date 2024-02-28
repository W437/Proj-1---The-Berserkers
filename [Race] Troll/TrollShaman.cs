// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollShaman : RangedUnit
{
    public TrollShaman() : base(damage: new Dice(2, 7, 3), hp: 70, armor: 10, range: 250)
    {
        UnitRace = Race.Troll;
        SetCritChance(0.15f);
        SetCritMultiplier(1.3f);
        SetCarryCapacity(50);
        SetEvasionChance(0.15f);
        EquippedWeapon = new ShamanicEnergyProjectile();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }
}
