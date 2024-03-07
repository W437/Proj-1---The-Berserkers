// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public abstract class Unit
{
    protected IRandomProvider RandomProvider { get; private set; }
    public Weapon? EquippedWeapon { get; set; }
/*    protected Dice Damage { get; set; }
    protected Dice HitChance { get; set; }
    protected Dice DefenseRating { get; set; }*/
    public int HP { get; private set; }
    protected int Armor { get; private set; } // armor mitigates damage
    public int CarryCapacity { get; private set; }
    public float CritChance { get; private set; }
    public float CritMultiplier { get; private set; }
    public void SetCarryCapacity(int v) => CarryCapacity = v;
    public void SetCritChance(float v) => CritChance = v;
    public void SetCritMultiplier(float v) => CritMultiplier = v;

    // Modifiers
    protected float HitChanceModifier { get; set; } = 0;
    protected float DefenseRatingModifier { get; set; } = 0;
    protected float RangeModifier { get; set; } = 0;


    protected Unit(IRandomProvider randomProvider, int hp, int armor)
    {
        HP = hp;
        Armor = armor;
        RandomProvider = randomProvider;
    }

    public void Attack(Unit target)
    {
        if (!IsAlive()) return;

        Console.Write($"{GetType().Name} attacks ");

        int baseChance = RandomProvider.Next(1, 6);

        if (baseChance <= HitChanceModifier + 2)
        {
            Console.WriteLine("and misses.");
            return;
        }

        float combinedCritChance = CritChance;

        if (this is AssassinUnit && ((AssassinUnit)this).IsStealth)
        {
            combinedCritChance += 0.2f; // Stealth mode increases crit chance (for assassin unit)
        }

        bool _isCritHit = combinedCritChance > RandomProvider.NextDouble();

        int weaponDamage = EquippedWeapon.Use(this, target, isCritHit: _isCritHit);
        int totalDamage = weaponDamage + RandomProvider.Next(1, 20);

        float combinedCritMultiplier = CritMultiplier + EquippedWeapon.CritMultiplierBoost;

        Console.Write($"using {EquippedWeapon.WeaponName} against {target.GetType().Name}");

        if (_isCritHit)
        {
            totalDamage = (int)(totalDamage * combinedCritMultiplier);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($" with a critical hit for {totalDamage} damage!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        else
        {
            Console.Write($" for ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{totalDamage}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" damage.");
        }

        Console.ForegroundColor = ConsoleColor.White;


        target.Defend(this, totalDamage);
    }
    

    public virtual void Defend(Unit attacker, int damageAmount)
    {
        if (IsAlive())
        {
            int finalDamage = 0;
            // General defense mechanism for all units - if (defense or stealth mode)

            int baseChance = RandomProvider.Next(1, 10);

            if (baseChance > DefenseRatingModifier + 7)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"[ !!! ] {GetType().Name} gracefully avoids the attack!\n");
                Console.ForegroundColor = ConsoleColor.White;

                if (this is AssassinUnit && ((AssassinUnit)this).IsStealth)
                {
                    ((AssassinUnit)this).IsStealth = false; // exit stealth mode after avoiding the attack
                }
                return;
            }
            else
            {
                // Handling damage
                if (this is AssassinUnit && ((AssassinUnit)this).IsStealth)
                {
                    // Reduce damage taken by half while in stealth for AssassinUnit
                    int damageReductionInStealth = Armor / 2;
                    finalDamage = Math.Max(0, Math.Abs(damageAmount - damageReductionInStealth));
                    ((AssassinUnit)this).IsStealth = false; // exit stealth mode after taking damage
                }
                else
                {
                    // Standard damage reduction for all units
                    finalDamage = Math.Max(0, Math.Abs(damageAmount - Armor));
                }
            }
            Console.Write($"{GetType().Name} receives ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{finalDamage}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" final damage.");

            TakeDamage(finalDamage);
        }
    }


    protected virtual void TakeDamage(int damage)
    {
        HP -= damage;
        Console.ForegroundColor = ConsoleColor.Red;
        if (HP <= 0)
        {
            HP = 0;
            Console.WriteLine($"[ X ] {GetType().Name} has been defeated!");
        }
/*        else
        {
            Console.WriteLine($"[{GetType().Name}'s remaining HP: {HP}]");
        }*/
        Console.ForegroundColor = ConsoleColor.White;
    }


    public virtual void Heal(int amount) { HP += amount; }
   

    public bool IsAlive() { return HP > 0; }


    public Race UnitRace { get; set; }


    public enum Race
    {
        Dwarf,
        Troll,
        Human,
    }


    public enum Weather
    {
        Sunny,
        Cloudy,
        Rainy,
        Snowy,
        Windy,
        Foggy
    }


    // keep track of weather modifications for reverting
    protected Stack<Action> revertList = new();

    public void ResetWeatherEffects()
    {
        while (revertList.Any())
        {
            var revertAction = revertList.Pop();
            revertAction.Invoke();
        }
    }

    public virtual void ApplyWeather(Weather effect)
    {
        // check is dis ranged?
        bool isRangedUnit = this is RangedUnit;
        RangedUnit? unit = null;
        if (isRangedUnit) unit = (RangedUnit)this;

        Console.ForegroundColor = ConsoleColor.Green;

        float originalHitChanceModifier = HitChanceModifier;
        float originalDefenseRatingModifier = DefenseRatingModifier;
        int? originalRange = null;

        switch (effect)
        {
            case Weather.Sunny:

                HitChanceModifier += -1; // (+) is difficulty in hitting (-) is easier
                DefenseRatingModifier += 1; // (+) increase threshold in avoiding attack

                if (isRangedUnit)
                {
                    originalRange = unit.Range;
                    unit.Range += 50;
                }

            break;

            case Weather.Cloudy:

                HitChanceModifier += 2;
                DefenseRatingModifier += 1;

                if (isRangedUnit)
                {
                    originalRange = unit.Range;
                    unit.Range -= 10;
                }

            break;

            case Weather.Rainy:

                HitChanceModifier += 2;
                DefenseRatingModifier += 2;

                if (isRangedUnit)
                {
                    originalRange = unit.Range;
                    unit.Range -= 30;
                }

            break;

            case Weather.Snowy:

                HitChanceModifier += 3;
                DefenseRatingModifier += -3;

                if (isRangedUnit)
                {
                    originalRange = unit.Range;
                    unit.Range -= 50;
                }

            break;

            case Weather.Windy:

                HitChanceModifier += 2;
                DefenseRatingModifier += 1;

                if (isRangedUnit)
                {
                    originalRange = unit.Range;
                    unit.Range -= 40;
                }

            break;

            case Weather.Foggy:

                HitChanceModifier += 3;
                DefenseRatingModifier += 2;

                if (isRangedUnit)
                {
                    originalRange = unit.Range;
                    unit.Range -= 80;
                }

            break;

            default:
            break;
        }

        // -- Weather changes to undo
        // All units
        revertList.Push(() => HitChanceModifier = originalHitChanceModifier);
        revertList.Push(() => DefenseRatingModifier = originalDefenseRatingModifier);

        // RangedUnit only
        if (isRangedUnit) revertList.Push(() => unit.Range = (int)originalRange);

        //ApplySpecificWeatherEffect(effect); // for (subclasses w unique properties (was for (Range) unused)
    }

}
