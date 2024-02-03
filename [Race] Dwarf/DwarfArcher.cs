// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class DwarfTank : RangedUnit
{
    public DwarfTank() : base(damage: new Dice(2, 6, 3), hp: 200, armor: 10, range: 200)
    {
        UnitRace = Race.Dwarf;
        CritChance = 0.10f;
        CritMultiplier = 1.5f;
        CurrentProjectile = CreateProjectile();
        CarryCapacity = 90;
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