// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class DwarfArcher : RangedUnit
{
    public DwarfArcher() : base(damage: new Dice(2, 7, 3), hp: 150, armor: 50, range: 300)
    {
        UnitRace = Race.Dwarf;
        CritChance = 0.20f;
        CritMultiplier = 2.0f;
        CurrentProjectile = CreateProjectile();
        CarryCapacity = 70;
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
    }

    public override void Attack(Unit target)
    {
        base.Attack(target);
    }

    public override void Defend(Unit attacker, int damageAmount)
    {
        base.Defend(attacker, damageAmount);
    }

    protected override string AttackMessage()
    {
        return "releases a powerful arrow!";
    }

    protected override string DefendMessage()
    {
        return "raises a shield in defense!";
    }

    protected override Projectile CreateProjectile()
    {
        return new ArrowProjectile(15);
    }

    public sealed class ArrowProjectile : Projectile
    {
        public ArrowProjectile(int baseDamage) : 
            base(baseDamage, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f) { }
    }
}