﻿// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class HumanSniper : AssassinUnit
{
    public HumanSniper() : base(damage: 25, hp: 110, armor: 50)
    {
        UnitRace = Race.Human;
        CritChance = 0.15f;
        CritMultiplier = 1.5f;
        EvasionChance += 0.1f;
        CurrentWeapon = CreateWeapon(typeof(PhantomSniper));
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
        return IsStealth ? "strikes from the darkness with increased damage!" : "attacks boldly!";
    }

    protected override string DefendMessage()
    {
        return "evades with finesse!";
    }

    public override AssassinWeapon CreateWeapon(Type weaponType)
    {
        switch (weaponType.Name)
        {
            case nameof(PhantomSniper):
                return new PhantomSniper();
            default:
                return null;
        }
    }

    public sealed class PhantomSniper : AssassinWeapon
    {
        public PhantomSniper() : base(baseDamage: 50, additionalCritChance: 0.10f, critMultiplierBoost: 0.3f) { }

        public override void UseWeapon(Unit attacker, Unit target, bool isStealthMode, int extraDamage = 0)
        {
            Console.WriteLine("Human Sniper performs a Swift Attack with the Phantom Sniper!");

            Random random = new Random(); 
            int randomValue = random.Next(1, 101);

            if (randomValue <= 20) // 20%
            {
                int swiftAttackDamage = (int)(Damage * 1.5f); // 1.5x damage in swift attack
                base.UseWeapon(attacker, target, isStealthMode, swiftAttackDamage);
                Console.WriteLine("Swift Attack! Damage 1.5X");
            }
            else
            {
                base.UseWeapon(attacker, target, isStealthMode);
                Console.WriteLine("Normal Attack.");
            }
        }
    }
}