﻿// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public sealed class TrollGunslinger : AssassinUnit
{
    public TrollGunslinger() : base(damage: new Dice(2, 7, 3), hp: 70, armor: 30)
    {
        UnitRace = Race.Troll;
        CritChance = 0.65f;
        CritMultiplier = 1.4f;
        EvasionChance += 0.29f;
        CarryCapacity = 70;
        CurrentWeapon = CreateWeapon(typeof(Shadowblade));
        DefenseRating = new Dice(1, 10, 0);
        HitChance = new Dice(1, 5, 0);
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
            Console.WriteLine("Troll Gunslinger has been defeated!");
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
        // protected const float LifestealPercentage = 0.15f; // 15% lifesteal

        public Trollstriker() : base(baseDamage: 30, additionalCritChance: 0.10f, critMultiplierBoost: 0.5f) { }

        public override void UseWeapon(Unit attacker, Unit target, bool isStealthMode, int extraDamage = 0)
        {
            base.UseWeapon(attacker, target, isStealthMode, extraDamage);            
        }
    }
}
