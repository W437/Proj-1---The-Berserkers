// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanRogue : AssassinUnit
{
    public HumanRogue() : base(damage: new Dice(2, 7, 3), hp: 70, armor: 10)
    {
        UnitRace = Race.Human;
        SetCritChance(0.10f);
        SetCritMultiplier(1.6f);
        SetCarryCapacity(50);
        SetEvasionChance(0.1f);
        EquippedWeapon = new VenomStinger();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

}
