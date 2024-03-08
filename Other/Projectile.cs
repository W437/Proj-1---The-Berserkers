// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public class Projectile : Weapon
{
    public int Range { get; set; }
    public Projectile(int baseDamage, float additionalCritChance, float critMultiplierBoost) : base(baseDamage, additionalCritChance, critMultiplierBoost) { }
}


public class ArrowProjectile : Projectile
{
    public ArrowProjectile() : base(baseDamage: 5, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f) 
    {
        WeaponName = "Arrow Projectile";
    }

}


public sealed class BoltProjectile : Projectile
{
    public BoltProjectile() : base(baseDamage: 4, additionalCritChance: 0.15f, critMultiplierBoost: 2.5f)
    {
        WeaponName = "Bolt Projectile";
    }
}


public sealed class ShamanicEnergyProjectile : Projectile
{
    private int consecutiveHits = 0;

    public ShamanicEnergyProjectile() : base(baseDamage: 9, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f)
    {
        WeaponName = "Shamanic Energy Projectile";
    }

    int GetConsecutiveHits()
    {
        return consecutiveHits;
    }

    void ResetConsecutiveHits()
    {
        consecutiveHits = 0;
    }

    void IncrementConsecutiveHits()
    {
        consecutiveHits++;
    }

    protected override void ApplyWeaponEffect(Unit attacker, Unit target, ref int damage, bool isCriticalHit)
    {
        // + damage for each consecutive hit
        damage += consecutiveHits * 2;
        if (GetConsecutiveHits() > 3)
        {
            ResetConsecutiveHits();
        }
        if (consecutiveHits > 0)
        {
            Console.WriteLine($"Streak {consecutiveHits} - Keep 'em coming!");
        }

        else
        {
            IncrementConsecutiveHits();
        }
    }


/*    public int Use(Unit attacker, Unit target, int extraDamage = 0)
    {
        int totalDamage = BaseDamage + attacker.RollDamage();
        float combinedCritChance = attacker.GetCritChance() + AdditionalCritChance;
        float combinedCritMultiplier = attacker.GetCritMultiplier() + CritMultiplierBoost;

        // + crit chance for each consecutive hit
        combinedCritChance += consecutiveHits * 0.05f;
        if (consecutiveHits > 0)
        {
            Console.WriteLine($"Streak {consecutiveHits} - Keep 'em coming!");
        }

        bool isCriticalHit = Random.Shared.NextDouble() < combinedCritChance;

        int finalDamage = isCriticalHit ? (int)(totalDamage * combinedCritMultiplier) : totalDamage;

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(isCriticalHit ? " [Critical hit!]" : " [Normal hit!]");
        Console.ForegroundColor = ConsoleColor.White;

        if (GetConsecutiveHits() > 3)
        {
            ResetConsecutiveHits();
        }
        else
        {
            IncrementConsecutiveHits();
        }

        return finalDamage;
    }*/
}