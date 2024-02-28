
public abstract class Weapon
{
    public int BaseDamage { get; private set; }
    public float AdditionalCritChance { get; set; }
    public float CritMultiplierBoost { get; private set; }
    public virtual string? WeaponName { get; set; }

    protected Weapon(int baseDamage, float additionalCritChance, float critMultiplierBoost)
    {
        BaseDamage = baseDamage;
        AdditionalCritChance = additionalCritChance;
        CritMultiplierBoost = critMultiplierBoost;
    }

    public virtual int Use(Unit attacker, Unit target, bool isStealthMode = false, int extraDamage = 0, bool isCritHit = false)
    {
        int totalDamage = BaseDamage + extraDamage; 

        int finalDamage = totalDamage;

        ApplyWeaponEffect(attacker, target, ref finalDamage, isCritHit); // Apply any specific weapon effects

        return finalDamage;
    }

    protected virtual void ApplyWeaponEffect(Unit attacker, Unit target, ref int damage, bool isCritHit) { }
}


public class Dagger : Weapon
{
    public Dagger() : base(baseDamage: 15, additionalCritChance: 0.15f, critMultiplierBoost: 0.13f) 
    {
        WeaponName = "Dagger";
    }
}
 

public sealed class PoisonedDagger : Dagger
{
    public PoisonedDagger() : base()
    {
        WeaponName = "Poisoned Dagger";
        AdditionalCritChance += 0.10f;
    }
}


public sealed class VenomStinger : Weapon
{
    private int swiftPercentage = 20;
    public VenomStinger() : base(baseDamage: 20, additionalCritChance: 0.15f, critMultiplierBoost: 0.05f)
    {
        WeaponName = "Venom Stinger";
    }

    protected override void ApplyWeaponEffect(Unit attacker, Unit target, ref int damage, bool isCriticalHit)
    {
        int rand = Random.Shared.Next(1, 101);

        if (rand <= swiftPercentage)
        {
            damage *= 2; // 2x damage in swift attack
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" [Swift Attack! Damage 2X]");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}


public sealed class PhantomSniper : Weapon
{
    private int swiftPercentage = 20;
    public PhantomSniper() : base(baseDamage: 25, additionalCritChance: 0.10f, critMultiplierBoost: 0.05f)
    {
        WeaponName = "Phantom Sniper";
    }

    protected override void ApplyWeaponEffect(Unit attacker, Unit target, ref int damage, bool isCriticalHit)
    {
        int rand = Random.Shared.Next(1, 101);

        if (rand <= swiftPercentage)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" [Swift Attack! Damage 1.5X]");
            Console.ForegroundColor = ConsoleColor.White;
            damage *= (int)1.5;
        }
    }
}


public sealed class TrollStriker : Weapon
{
    private const float lifestealPercentage = 0.15f; // 15% lifesteal

    public TrollStriker() : base(baseDamage: 20, additionalCritChance: 0.10f, critMultiplierBoost: 0.05f)
    {
        WeaponName = "Troll Striker";
    }

    protected override void ApplyWeaponEffect(Unit attacker, Unit target, ref int damage, bool isCriticalHit)
    {
        int lifestealAmount = (int)(damage * lifestealPercentage);
        attacker.Heal(lifestealAmount);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{target} left puzzled! TrollScout chants 'Abracadabra, health for me!' and scores {lifestealAmount} HP! Trolltastic!");
        Console.ForegroundColor = ConsoleColor.White;
    }
}


public sealed class Shadowblade : Weapon
{
    public Shadowblade() : base(baseDamage: 15, additionalCritChance: 0.20f, critMultiplierBoost: 0.1f)
    {
        WeaponName = "Shadow Blade";
    }
}

