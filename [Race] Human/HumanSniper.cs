// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanSniper : AssassinUnit
{
    public HumanSniper() : base(new Dice(2, 7, 3), new Dice(1, 5, 0), new Dice(1, 10, 0), hp: 80, armor: 15)
    {
        UnitRace = Race.Human;
        SetCritChance(0.15f);
        SetCritMultiplier(1.2f);
        SetCarryCapacity(50);
        SetEvasionChance(0.15f);
        EquippedWeapon = new PhantomSniper();
    }
}
