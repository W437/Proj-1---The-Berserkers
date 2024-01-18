// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class HumanCrossbowman : RangedUnit
{
    public HumanCrossbowman() : base(damage: 15, hp: 180, armor: 35, range: 400)
    {
        UnitRace = Unit.Race.Human;
        CritChance = 0.40f;
        CritMultiplier = 1.2f;
    }

    public override void Attack(Unit target)
    {
        Console.WriteLine("Human Crossbowman fires a deadly bolt!");
        Projectile projectile = CreateProjectile();
        projectile.Hit(this, target);
    }

    public override void Defend(Unit attacker, int damageAmount)
    {
        base.Defend(attacker, damageAmount);
        Console.WriteLine("Human Crossbowman takes cover and defends!");
        ReceiveDamage(attacker.Damage);
    }

    public override void ReceiveDamage(int amount)
    {
        int damageReduction = Armor;
        HP -= Math.Max(0, amount - damageReduction);

        if (HP <= 0)
        {
            Console.WriteLine("Human Crossbowman has been defeated!");
        }
    }

    protected override Projectile CreateProjectile()
    {
        return new BoltProjectile(Damage);
    }

    public sealed class BoltProjectile : Projectile
    {
        public BoltProjectile(int baseDamage) :
            base(baseDamage, additionalCritChance: 0.15f, critMultiplierBoost: 2.5f)
        { }
    }

}
