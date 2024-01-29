// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public abstract class AssassinUnit : Unit
{
    protected virtual bool IsStealth { get; set; }
    protected virtual AssassinWeapon? CurrentWeapon { get; set; }

    protected AssassinUnit(int damage, int hp, int armor) : base(damage, hp, armor) 
    {
        IsStealth = true;
    }

    public override void Attack(Unit target)
    {   
        string unitName = this.GetType().Name;

        if (IsStealth)
        {
            Console.WriteLine($"{unitName} {AttackMessage(true)}");
            IsStealth = false;
            CurrentWeapon.UseWeapon(this, target, IsStealth);
        }
        else
        {
            Console.WriteLine($"{unitName} {AttackMessage()}");
            CurrentWeapon.UseWeapon(this, target, IsStealth);
        }
    }

    public override void Defend(Unit attacker, int damageAmount)
    {
        string unitName = this.GetType().Name;

        Random random = new Random();
        double randomEvasion = random.NextDouble();

        if (randomEvasion <= EvasionChance)
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
        string unitName = this.GetType().Name;
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

            if(weaponName == "TrollStriker")
            {

                int lifestealAmount = (int)(finalDamage * attLifestealPercentage);
                attacker.ReceiveHealing(lifestealAmount);
                Console.WriteLine($"{target} left puzzled! TrollScout chants 'Abracadabra, health for me!' and scores {lifestealAmount} HP! Trolltastic!");
            }
        }

        public static implicit operator AssassinWeapon(Type v)
        {
            throw new NotImplementedException();
        }
    }
}
