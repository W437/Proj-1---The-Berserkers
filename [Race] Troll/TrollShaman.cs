// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

using static TrollScout;

public sealed class TrollShaman : RangedUnit
{
    public TrollShaman() : base(damage: 15, hp: 150, armor: 80, range: 250)
    {
        UnitRace = Race.Troll;
        CritChance = 0.40f;
        CritMultiplier = 1.4f;
        CurrentProjectile = CreateProjectile();
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
        return "casts a powerful spell!";
    }

    protected override string DefendMessage()
    {
        return "evades the attack!";
    }

    protected override Projectile CreateProjectile()
    {
        return new ShamanicEnergyProjectile(Damage);
    }

    public sealed class ShamanicEnergyProjectile : Projectile
    {
        private int consecutiveHits = 0;

        public ShamanicEnergyProjectile(int baseDamage) :
            base(baseDamage, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f)
        { }

        public int GetConsecutiveHits()
        {
            return consecutiveHits;
        }

        public void ResetConsecutiveHits()
        {
            consecutiveHits = 0;
        }

        public void IncrementConsecutiveHits()
        {
            consecutiveHits++;
        }

        public override void Hit(Unit attacker, Unit target)
        {
            float combinedCritChance = attacker.CritChance + AdditionalCritChance;
            float combinedCritMultiplier = attacker.CritMultiplier + CritMultiplierBoost;

            // + crit chance for each consecutive hit
            combinedCritChance += consecutiveHits * 0.05f;
            if (consecutiveHits > 0)
            {
                Console.WriteLine($"Streak {consecutiveHits} - Keep 'em coming!");
            }

            Random random = new Random();
            bool isCriticalHit = random.NextDouble() < combinedCritChance;

            int totalDamage = Damage + attacker.Damage;
            int finalDamage = isCriticalHit ? (int)(totalDamage * combinedCritMultiplier) : totalDamage;

            Console.WriteLine(isCriticalHit ? "Critical hit!" : "Normal hit!");

            target.ReceiveDamage(finalDamage);

            if(GetConsecutiveHits() > 3) 
            { 
                ResetConsecutiveHits();
            }
            else
            {
                IncrementConsecutiveHits();
            }
        }
    }
}
