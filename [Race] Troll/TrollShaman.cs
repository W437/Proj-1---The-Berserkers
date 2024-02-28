// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollShaman : RangedUnit
{
    public TrollShaman() : base(damage: new Dice(2, 7, 3), hp: 70, armor: 80, range: 250)
    {
        UnitRace = Race.Troll;
        CritChance = 0.40f;
        CritMultiplier = 1.4f;
        CarryCapacity = 50;
        EquippedWeapon = new ShamanicEnergyProjectile();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }
}
