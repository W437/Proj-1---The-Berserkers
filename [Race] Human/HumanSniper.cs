// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanSniper : AssassinUnit
{
    public HumanSniper() : base(damage: new Dice(2, 7, 3), hp: 70, armor: 13)
    {
        UnitRace = Race.Human;
        SetCritChance(0.15f);
        SetCritMultiplier(1.2f);
        SetCarryCapacity(50);
        SetEvasionChance(0.15f);
        EquippedWeapon = new PhantomSniper();
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

}
