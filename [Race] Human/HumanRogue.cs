// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanRogue : AssassinUnit
{
    public HumanRogue() : base(damage: new Dice(2, 7, 3), hp: 70, armor: 50)
    {
        UnitRace = Race.Human;
        CritChance = 0.15f;
        CritMultiplier = 1.5f;
        EvasionChance += 0.1f;
        CarryCapacity = 50;
        EquippedWeapon = new VenomStinger();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

}
