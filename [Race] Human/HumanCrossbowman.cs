// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanCrossbowman : RangedUnit
{
    public HumanCrossbowman() : base(damage: new Dice(2, 6, 3), hp: 100, armor: 35, range: 400)
    {
        UnitRace = Race.Human;
        CritChance = 0.40f;
        CritMultiplier = 1.2f;
        CarryCapacity = 70;
        CurrentProjectile = CreateProjectile();
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
        return "fires a deadly bolt!";
    }

    protected override string DefendMessage()
    {
        return "takes cover and defends!";
    }

    protected override Projectile CreateProjectile()
    {
        return new BoltProjectile(15);
    }

    public sealed class BoltProjectile : Projectile
    {
        public BoltProjectile(int baseDamage) :
            base(baseDamage, additionalCritChance: 0.15f, critMultiplierBoost: 2.5f)
        { }
    }

}
