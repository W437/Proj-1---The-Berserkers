// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public abstract class AssassinUnit : Unit
{
    protected virtual bool IsStealth { get; set; }
    protected virtual AssassinWeapon? CurrentWeapon { get; set; }

    protected AssassinUnit(Dice damage, int hp, int armor) : base(damage, hp, armor) 
    {
        IsStealth = true;
    }

    public override void Attack(Unit target)
    {   
        string unitName = GetType().Name;

        if(HitChance.Roll() > 4) // miss chance 1/5
        {
            Console.WriteLine($"{unitName} misses.");
        }
        else
        {
            if (IsStealth)
            {
                Console.WriteLine($"{unitName} {AttackMessage(true)}");
                IsStealth = false;
                CurrentWeapon?.UseWeapon(this, target, IsStealth);
            }
            else
            {
                Console.WriteLine($"{unitName} {AttackMessage()}");
                CurrentWeapon?.UseWeapon(this, target, IsStealth);
            }
        }
    }

    public override void Defend(Unit attacker, int damageAmount)
    {
        string unitName = GetType().Name;

        double randomEvasion = Random.Shared.Next(1, 11);

        if (randomEvasion <= DefenseRating.Roll())
        {
            Console.WriteLine($"{unitName} gracefully avoids the attack!");
        }

        else
        {
            int finalDamage;
            if (IsStealth)
            {
                // Reduce damage taken by half while in stealth
                int damageReductionInStealth = Armor / 2;
                finalDamage = Math.Max(0, damageAmount - damageReductionInStealth);
                IsStealth = false;
            }
            else
            {
                int damageReduction = Armor;
                finalDamage = Math.Max(0, damageAmount - damageReduction);
            }

            ReceiveDamage(finalDamage);
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

    protected virtual string AttackMessage(bool IsStealth = false)
    {
        return "attacks!";
    }

    protected virtual string DefendMessage()
    {
        return "evades the attack!";
    }

    public void Hide()
    {
        IsStealth = true;
    }

    public override void WeatherEffect(WeatherEffects effect)
    {
        Console.WriteLine($"Weather changed to: {effect}");
        switch (effect)
        {
            case WeatherEffects.Sunny:
                HitChance.ModifyModifier(1);
                break;

            case WeatherEffects.Cloudy:
                EvasionChance -= 0.1f;
                break;

            case WeatherEffects.Rainy:
                DefenseRating.ModifyModifier(-1);
                break;

            case WeatherEffects.Snowy:
                DefenseRating.ModifyModifier(-2);
                break;

            case WeatherEffects.Windy:
                DefenseRating.ModifyModifier(-1);
                break;

            case WeatherEffects.Foggy:
                EvasionChance += 0.15f;
                break;

            default:
                Console.WriteLine("No weather effect applied.");
                break;
        }
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
            int totalDamage = attacker.Damage.Roll() + extraDamage;
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

            switch(weaponName)
            {
                case "TrollStriker":
                    int lifestealAmount = (int)(finalDamage * 0.15f); // todo: get the predetermined percentage from the attacker's weapon
                    attacker.ReceiveHealing(lifestealAmount);
                    Console.WriteLine($"{target} left puzzled! TrollScout chants 'Abracadabra, health for me!' and scores {lifestealAmount} HP! Trolltastic!");
                break;
            }
        }
    }
}
