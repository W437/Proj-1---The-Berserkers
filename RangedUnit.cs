// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public abstract class RangedUnit : Unit
{
    protected float Range { get; set; }

    protected RangedUnit(int damage, int hp, int armor, float range) : base(damage, hp, armor)
    {
        Range = range;
    }

    public override void Attack(Unit target)
    {
        Console.WriteLine("Ranged unit attacks!");
    }

    public override void Defend(Unit attacker, int damageAmount)
    {
        Console.WriteLine("Ranged unit defends!");
    }

    protected abstract Projectile CreateProjectile();


    public abstract class Projectile
    {
        public int Damage { get; set; }
        public float AdditionalCritChance { get; private set; }
        public float CritMultiplierBoost { get; private set; }

        protected Projectile(int baseDamage, float additionalCritChance = 0, float critMultiplierBoost = 0)
        {
            Damage = baseDamage;
            AdditionalCritChance = additionalCritChance;
            CritMultiplierBoost = critMultiplierBoost;
        }

        public virtual void Hit(Unit attacker, Unit target)
        {
            int totalDamage = Damage + attacker.Damage;
            float combinedCritChance = attacker.CritChance + AdditionalCritChance;
            float combinedCritMultiplier = attacker.CritMultiplier + CritMultiplierBoost;
        
            Random random = new Random();
            bool isCriticalHit = random.NextDouble() < combinedCritChance;
            int finalDamage = isCriticalHit ? (int)(totalDamage * combinedCritMultiplier) : totalDamage;

            Console.WriteLine(isCriticalHit ? "Critical hit!" : "Normal hit!");
            target.ReceiveDamage(finalDamage);
        }
    }
}
