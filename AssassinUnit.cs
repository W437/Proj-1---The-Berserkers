// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public abstract class AssassinUnit : Unit
{
    protected virtual bool IsStealth { get; set; }

    protected AssassinUnit(int damage, int hp, int armor) : base(damage, hp, armor)
    {
        IsStealth = true;
    }

    public override void Attack(Unit target)
    {
        Console.WriteLine("Assassin unit attacks!");
    }

    public override void Defend(Unit attacker, int damangeAmount)
    {
        Console.WriteLine("Assassin unit evades the attack!");
    }

    public void Hide()
    {
        IsStealth = true;
    }

    public abstract AssassinWeapon CreateWeapon(Type weaponType);

    public abstract class AssassinWeapon
    {
        public int Damage { get; private set; }
        public float AdditionalCritChance { get; private set; }
        public float CritMultiplierBoost { get; private set; }

        protected AssassinWeapon(int baseDamage, float additionalCritChance = 0.15f, float critMultiplierBoost = 0.55f)
        {
            Damage = baseDamage;
            AdditionalCritChance = additionalCritChance;
            CritMultiplierBoost = critMultiplierBoost;
        }

        public virtual void UseWeapon(Unit attacker, Unit target, bool isStealthMode = false, int extraDamage = 0)
        {
            string weaponName = GetType().Name;
            int totalDamage = Damage + attacker.Damage + extraDamage;
            float combinedCritChance = attacker.CritChance + AdditionalCritChance;
            float combinedCritMultiplier = attacker.CritMultiplier + CritMultiplierBoost;

            if (isStealthMode)
            {
                combinedCritChance += 0.25f;
            }

            Random random = new Random();
            bool isCriticalHit = random.NextDouble() < combinedCritChance;
            int finalDamage = isCriticalHit ? (int)(totalDamage * combinedCritMultiplier) : totalDamage;

            Console.WriteLine($"{attacker} attacks with {weaponName}!");
            Console.WriteLine(isCriticalHit ? "Critical hit!" : "Normal hit!");
            target.Defend(attacker, finalDamage);
        }
    }
}
