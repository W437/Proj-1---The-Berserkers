// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

using static AssassinUnit;

public abstract class RangedUnit : Unit
{   
    protected float Range { get; set; }
    protected Projectile? CurrentProjectile { get; set; }
    protected RangedUnit(Dice damage, int hp, int armor, float range) : base(damage, hp, armor)
    {
        Range = range;
        Damage = damage;
    }

    public override void Attack(Unit target)
    {
        string unitName = this.GetType().Name;
        Console.WriteLine($"{unitName} {AttackMessage()}");
        Projectile projectile = CreateProjectile();

        if(HitChance.Roll() > 4) // miss chance 1/5
        {
            Console.WriteLine($"{unitName} misses.");
        }
        else
        {
            projectile.Hit(this, target);
        }
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
        string unitName = GetType().Name;

        double randomEvasion = Random.Shared.Next(1, 11);

        if (randomEvasion <= DefenseRating.Roll())
        {
            Console.WriteLine($"{unitName} avoids the attack!");
        }
        else
        {
            Console.WriteLine($"{unitName} {DefendMessage()}");
            ReceiveDamage(damageAmount);
        }
    }

    public override void ReceiveDamage(int amount)
    {
        string unitName = GetType().Name;
        int damageReduction = Armor;
        HP -= Math.Max(0, amount - damageReduction);

        if (HP <= 0)
        {
            Console.WriteLine($"{unitName} has been defeated!");
        }
    }

    public override void WeatherEffect(WeatherEffects effect)
    {
        Console.WriteLine($"Weather changed to: {effect}");
        switch (effect)
        {
            case WeatherEffects.Sunny:
                HitChance.ModifyModifier(1);
                Range += 30f;
                Console.WriteLine("Sunny weather improves hit chance and range.");
                break;

            case WeatherEffects.Cloudy:
                EvasionChance -= 0.1f;
                Console.WriteLine("Cloudy weather reduces evasion chance.");
                break;

            case WeatherEffects.Rainy:
                DefenseRating.ModifyModifier(-1);
                Console.WriteLine("Rainy weather decreases defense rating.");
                break;

            case WeatherEffects.Snowy:
                DefenseRating.ModifyModifier(-2);
                Console.WriteLine("Snowy weather significantly decreases defense rating.");
                break;

            case WeatherEffects.Windy:
                Range -= 40;
                Console.WriteLine("Windy weather reduces range.");
                break;

            case WeatherEffects.Foggy:
                Range -= 80;
                EvasionChance += 0.15f;
                Console.WriteLine("Foggy weather greatly reduces range and increases evasion chance.");
                break;

            default:
                Console.WriteLine("No weather effect applied.");
                break;
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
            // Roll the dice for both attacker's and unit's damage
            int attackerDamage = attacker.Damage.Roll(true);
            int totalDamage = attackerDamage;

            float combinedCritChance = attacker.CritChance + AdditionalCritChance;
            float combinedCritMultiplier = attacker.CritMultiplier + CritMultiplierBoost;

            Random random = new Random();
            bool isCriticalHit = random.NextDouble() < combinedCritChance;

            // Apply critical hit multiplier if it's a critical hit
            int finalDamage = isCriticalHit ? (int)(totalDamage * combinedCritMultiplier) : totalDamage;

            Console.WriteLine(isCriticalHit ? "Critical hit!" : "Normal hit!");
            target.Defend(attacker, finalDamage);
        }
    }   
}       
