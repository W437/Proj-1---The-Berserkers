// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class TrollScout : AssassinUnit
{
    public TrollScout() : base(damage: 10, hp: 60, armor: 80)
    {
        UnitRace = Race.Troll;
        CritChance = 0.40f;
        CritMultiplier = 1.4f;
        EvasionChance += 0.25f;
        CurrentWeapon = typeof(Trollstriker);
    }

    public override void Attack(Unit target)
    {
        base.Attack(target);
    }

    public override void Defend(Unit attacker, int damageAmount)
    {
        base.Defend(attacker, damageAmount);
    }

    protected override string AttackMessage(bool IsStealth)
    {
        return IsStealth ? "performs a precise assassination with increased damage!" : "attacks normally!";
    }


    protected override string DefendMessage()
    {
        return "evades the attack!";
    }

    public override void ReceiveDamage(int amount)
    {
        int damageReduction = Armor;
        HP -= Math.Max(0, amount - damageReduction);

        if (HP <= 0)
        {
            Console.WriteLine("Troll Scout has been defeated!");
        }
    }

    public override AssassinWeapon CreateWeapon(Type weaponType)
    {
        switch (weaponType.Name)
        {
            case nameof(Shadowblade):
                return new Shadowblade();

            case nameof(Trollstriker):
                return new Trollstriker();
            default:
                return null;
        }
    }


    public sealed class Shadowblade : AssassinWeapon
    {
        public Shadowblade() : base(baseDamage: 35, additionalCritChance: 0.20f) { }
    }


    public sealed class Trollstriker : AssassinWeapon
    {
        private const float LifestealPercentage = 0.15f; // 15% lifesteal

        public Trollstriker() : base(baseDamage: 30, additionalCritChance: 0.10f, critMultiplierBoost: 0.5f) { }

        public override void UseWeapon(Unit attacker, Unit target, bool isStealthMode, int extraDamage = 0)
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

            target.ReceiveDamage(finalDamage);

            
        }
    }
}
