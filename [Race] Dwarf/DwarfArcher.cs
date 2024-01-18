// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class DwarfArcher : RangedUnit
{
    public DwarfArcher() : base(damage: 15, hp: 80, armor: 50, range: 300)
    {
        UnitRace = Unit.Race.Dwarf;
        CritChance = 0.20f;
        CritMultiplier = 2.0f;
    }

    public override void Attack(Unit target)
    {
        Console.WriteLine("Dwarf Archer releases a powerful arrow!");
        Projectile projectile = CreateProjectile();
        projectile.Hit(this, target);
    }

    public override void Defend(Unit attacker, int damageAmount)
    {
        base.Defend(attacker, damageAmount);
        Console.WriteLine("Dwarf Archer raises a shield in defense!");
        ReceiveDamage(attacker.Damage);
    }

    public override void ReceiveDamage(int amount)
    {
        int damageReduction = Armor;
        HP -= Math.Max(0, amount - damageReduction);

        if (HP <= 0)
        {
            Console.WriteLine("Dwarf Archer has been defeated!");
        }
    }

    protected override Projectile CreateProjectile()
    {
        return new ArrowProjectile(Damage);
    }

    public sealed class ArrowProjectile : Projectile
    {
        public ArrowProjectile(int baseDamage) : 
            base(baseDamage, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f) { }
    }
}
