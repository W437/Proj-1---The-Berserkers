// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanRogue : AssassinUnit
{
    public HumanRogue() : base(new Dice(2, 7, 3), new Dice(1, 5, 0), new Dice(1, 10, 0), hp: 70, armor: 10)
    {
        UnitRace = Race.Human;
        SetCritChance(0.10f);
        SetCritMultiplier(1.6f);
        SetCarryCapacity(50);
        SetEvasionChance(0.1f);
        EquippedWeapon = new VenomStinger();
    }
}
