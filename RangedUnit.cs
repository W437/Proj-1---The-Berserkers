// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

using static AssassinUnit;

public abstract class RangedUnit : Unit
{   
    protected float Range { get; set; }
    protected Projectile? CurrentProjectile { get; set; }
    protected RangedUnit(int damage, int hp, int armor, float range) : base(damage, hp, armor)
    {
        Range = range;
    }

    public override void Attack(Unit target)
    {
        string unitName = this.GetType().Name;
        Console.WriteLine($"{unitName} {AttackMessage()}");
        Projectile projectile = CreateProjectile();
        projectile.Hit(this, target);
    }

    protected virtual string AttackMessage()
    {
        return "releases a powerful arrow!";
    }

    protected virtual string DefendMessage()
    {
        return "takes cover and defends!";
    }

    public override void Defend(Unit attacker, int damageAmount)
    {
        string unitName = this.GetType().Name;
        Console.WriteLine($"{unitName} {DefendMessage()}");
        ReceiveDamage(attacker.Damage);
    }

    public override void ReceiveDamage(int amount)
    {
        string unitName = this.GetType().Name;
        int damageReduction = Armor;
        HP -= Math.Max(0, amount - damageReduction);

        if (HP <= 0)
        {
            Console.WriteLine($"{unitName} has been defeated!");
        }
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
