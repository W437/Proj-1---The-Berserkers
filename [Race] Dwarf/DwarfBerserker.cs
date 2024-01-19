// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class DwarfBerserker : AssassinUnit
{
    public DwarfBerserker() : base(damage: 25, hp: 150, armor: 30)
    {
        UnitRace = Race.Dwarf;
        CritChance = 0.35f;
        CritMultiplier = 1.7f;
        EvasionChance += 0.08f;
    }

    public override void Attack(Unit target)
    {
        base.Attack(target);
        AssassinWeapon weapon = CreateWeapon(typeof(Dagger));

        if (IsStealth)
        {
            Console.WriteLine("Dwarf Berserker strikes from the shadows with increased damage!");
            IsStealth = false;
            weapon.UseWeapon(this, target, IsStealth);
        }
        else
        {
            Console.WriteLine("Dwarf Berserker attacks normally.");
            weapon.UseWeapon(this, target, IsStealth);
        }
    }

    public override void Defend(Unit attacker, int damageAmount)
    {

        Random random = new Random();
        double randomEvasion = random.NextDouble();

        if (randomEvasion <= EvasionChance)
        {
            Console.WriteLine("Dwarf Berserker gracefully avoids the attack!");
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
        int damageReduction = Armor;
        HP -= Math.Max(0, amount - damageReduction);

        if (HP <= 0)
        {
            Console.WriteLine("Dwarf Berserker has been defeated!");
        }
    }

    public override AssassinWeapon CreateWeapon(Type weaponType)
    {
        switch (weaponType.Name)
        {
            case nameof(Dagger):
                return new Dagger();

            case nameof(PoisonedDagger):
                return new PoisonedDagger();

            default:
                return null;
        }
    }


    public sealed class Dagger : AssassinWeapon
    {
        bool isOnCooldown = false;

        public Dagger() : base(baseDamage: 50, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f) { }

        // Swift attack!
        public override void UseWeapon(Unit attacker, Unit target, bool isStealthMode, int extraDamage = 0)
        {
            if (isOnCooldown)
            {
                Console.WriteLine("Dagger is on cooldown. Cannot perform Swift Attack.");
            }
            else
            {
                Console.WriteLine("Dwarf Berserker performs a Swift Attack with the Dagger!");
                base.UseWeapon(attacker, target, isStealthMode);

                isOnCooldown = true;
                Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith(_ => isOnCooldown = false);
            }
        }
    }

    public sealed class PoisonedDagger : AssassinWeapon
    {
        private const int DamagePerSecond = 3;
        private const int DurationInSeconds = 10;

        public PoisonedDagger() : base(baseDamage: 10, additionalCritChance: 0.1f, critMultiplierBoost: 0.4f) { }

        public override void UseWeapon(Unit attacker, Unit target, bool isStealthMode, int extraDamage = 0)
        {
            Console.WriteLine("Human Rogue performs an attack with the Poisoned Dagger!");

            int totalDamage = Damage + attacker.Damage + extraDamage;
            float combinedCritChance = attacker.CritChance + AdditionalCritChance;
            float combinedCritMultiplier = attacker.CritMultiplier + CritMultiplierBoost;

            Random random = new Random();
            bool isCriticalHit = random.NextDouble() < combinedCritChance;
            int finalDamage = isCriticalHit ? (int)(totalDamage * combinedCritMultiplier) : totalDamage;

            Console.WriteLine($"{attacker} attacks with Poisoned Dagger!");
            Console.WriteLine(isCriticalHit ? "Critical hit!" : "Normal hit!");

            // initial damage
            target.ReceiveDamage(finalDamage);

            // damage-over-time (DoT) effect
            Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(_ => ApplyDoTEffect(target, isStealthMode));
        }

        private static void ApplyDoTEffect(Unit target, bool isStealthMode)
        {
            for (int i = 0; i < DurationInSeconds; i++)
            {
                int dotDamage = DamagePerSecond + (isStealthMode ? (DamagePerSecond/2) : 0);
                Console.WriteLine($"Poison deals {dotDamage} damage over time.");

                target.ReceiveDamage(dotDamage);

                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }

            Console.WriteLine("Poisoned Dagger's damage-over-time effect has ended.");
        }
    }

}
