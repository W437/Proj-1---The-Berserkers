
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

    public virtual int Use(Unit attacker, Unit target, bool isStealthMode = false, int extraDamage = 0)
    {
        int totalDamage = BaseDamage + extraDamage; 
        float combinedCritChance = attacker.CritChance + AdditionalCritChance;
        float combinedCritMultiplier = attacker.CritMultiplier + CritMultiplierBoost;

        if (isStealthMode)
        {
            combinedCritChance += 0.25f; // Stealth mode increases crit chance
        }

        bool isCriticalHit = Random.Shared.NextDouble() < combinedCritChance;
        int finalDamage = isCriticalHit ? (int)(totalDamage * combinedCritMultiplier) : totalDamage;

        ApplyWeaponEffect(attacker, target, ref finalDamage, isCriticalHit); // Apply any specific weapon effects

        Console.WriteLine($"{attacker.GetType().Name} uses {WeaponName} against {target.GetType().Name} for {finalDamage} damage{(isCriticalHit ? " with a CRITICAL HIT!" : "")}.");

        return finalDamage;
    }

    protected virtual void ApplyWeaponEffect(Unit attacker, Unit target, ref int damage, bool isCriticalHit) { }
}


public class Dagger : Weapon
{
    public Dagger() : base(baseDamage: 50, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f) 
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
    public VenomStinger() : base(baseDamage: 30, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f)
    {
        WeaponName = "Venom Stinger";
    }

    protected override void ApplyWeaponEffect(Unit attacker, Unit target, ref int damage, bool isCriticalHit)
    {
        int rand = Random.Shared.Next(1, 101);

        if (rand <= swiftPercentage)
        {
            damage *= 2; // 2x damage in swift attack
            Console.WriteLine("Swift Attack! Damage X2!");
        }
        else
        {
            Console.WriteLine("Normal Attack.");
        }
    }
}


public sealed class PhantomSniper : Weapon
{
    private int swiftPercentage = 20;
    public PhantomSniper() : base(baseDamage: 50, additionalCritChance: 0.10f, critMultiplierBoost: 0.3f)
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

    public TrollStriker() : base(baseDamage: 30, additionalCritChance: 0.10f, critMultiplierBoost: 0.5f)
    {
        WeaponName = "Troll Striker";
    }

    protected override void ApplyWeaponEffect(Unit attacker, Unit target, ref int damage, bool isCriticalHit)
    {
        int lifestealAmount = (int)(damage * lifestealPercentage);
        attacker.Heal(lifestealAmount);
        Console.WriteLine($"{target} left puzzled! TrollScout chants 'Abracadabra, health for me!' and scores {lifestealAmount} HP! Trolltastic!");
    }
}


public sealed class Shadowblade : Weapon
{
    public Shadowblade() : base(baseDamage: 35, additionalCritChance: 0.20f, critMultiplierBoost: 0.5f)
    {
        WeaponName = "Shadow Blade";
    }
}

