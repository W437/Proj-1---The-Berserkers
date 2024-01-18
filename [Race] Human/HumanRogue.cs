// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class HumanRogue : AssassinUnit
{
    public HumanRogue() : base(damage: 25, hp: 110, armor: 50)
    {
        UnitRace = Unit.Race.Human;
        CritChance = 0.15f;
        CritMultiplier = 1.5f;
        EvasionChance += 0.1f;
    }

    public override void Attack(Unit target)
    {
        base.Attack(target);
        AssassinWeapon weapon = CreateWeapon(typeof(VenomStinger));

        if (IsStealth)
        {
            Console.WriteLine("HumanRogue strikes from the shadows with increased damage!");
            IsStealth = false;
            weapon.UseWeapon(this, target, IsStealth);
        }
        else
        {
            Console.WriteLine("HumanRogue attacks normally.");
            weapon.UseWeapon(this, target, IsStealth);
        }
    }

    public override void Defend(Unit attacker, int damageAmount)
    {

        Random random = new Random();
        double randomEvasion = random.NextDouble();

        if (randomEvasion <= EvasionChance)
        {
            Console.WriteLine("Human Rogue evades with finesse!");
        }

        else
        {
            int finalDamage;
            if (IsStealth)
            {
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
            Console.WriteLine("Human Rogue has been defeated!");
        }
    }

    public override AssassinWeapon CreateWeapon(Type weaponType)
    {
        switch (weaponType.Name)
        {
            case nameof(VenomStinger):
                return new VenomStinger();
            default:
                return null;
        }
    }

    public sealed class VenomStinger : AssassinWeapon
    {
        public VenomStinger() : base(baseDamage: 30, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f) { }

        public override void UseWeapon(Unit attacker, Unit target, bool isStealthMode, int extraDamage = 0)
        {
            Console.WriteLine("Human Rogue performs a Swift Attack with the Venom Stinger!");

            Random random = new Random();
            int randomValue = random.Next(1, 101);

            if (randomValue <= 20) // 20%
            {
                int swiftAttackDamage = Damage * 2; // 2x damage in swift attack
                base.UseWeapon(attacker, target, isStealthMode, swiftAttackDamage);
                Console.WriteLine("Swift Attack! Damage X2!");
            }
            else
            {
                base.UseWeapon(attacker, target, isStealthMode);
                Console.WriteLine("Normal Attack.");
            }
        }
    }
}
