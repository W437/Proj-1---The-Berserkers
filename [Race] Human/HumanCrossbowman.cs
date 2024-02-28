// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanCrossbowman : RangedUnit
{
    public HumanCrossbowman() : base(damage: new Dice(2, 6, 3), hp: 65, armor: 35, range: 400)
    {
        UnitRace = Race.Human;
        CritChance = 0.40f;
        CritMultiplier = 1.2f;
        CarryCapacity = 70;
        EquippedWeapon = new BoltProjectile();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }
}
