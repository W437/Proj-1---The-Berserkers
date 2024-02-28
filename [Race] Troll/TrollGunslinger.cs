// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class TrollGunslinger : AssassinUnit
{
    public TrollGunslinger() : base(damage: new Dice(2, 7, 3), hp: 70, armor: 30)
    {
        UnitRace = Race.Troll;
        CritChance = 0.65f;
        CritMultiplier = 1.4f;
        EvasionChance += 0.29f;
        CarryCapacity = 70;
        EquippedWeapon = new Shadowblade();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

}
