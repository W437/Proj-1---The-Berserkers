// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public abstract class Unit
{
/*    protected IRandomProvider RandomProvider { get; private set; }*/
    public Weapon? EquippedWeapon { get; set; }
    protected Dice Damage { get; set; }
    protected Dice HitChance { get; set; }
    protected Dice DefenseRating { get; set; }
    public int HP { get; private set; }
    protected int Armor { get; private set; } // armor mitigates damage
    public int CarryCapacity { get; private set; }
    public float CritChance { get; private set; }
    public float CritMultiplier { get; private set; }
    public virtual float EvasionChance { get; private set; }
    public void SetCarryCapacity(int v) => CarryCapacity = v;
    public void SetCritChance(float v) => CritChance = v;
    public void SetCritMultiplier(float v) => CritMultiplier = v;
    public void SetEvasionChance(float v) => EvasionChance = v;

    protected Unit(Dice damage, Dice hitChance, Dice defense, int hp, int armor)
    {
        HP = hp;
        Armor = armor;
        Damage = damage;
        HitChance = hitChance;
        DefenseRating = defense;
    }

    public void Attack(Unit target)
    {
        if (!IsAlive()) return;

        Console.Write($"{GetType().Name} attacks ");

        if (Damage.Roll() <= 1)
        {
            Console.WriteLine("and misses.");
            return;
        }

        float combinedCritChance = CritChance;

        if (this is AssassinUnit && ((AssassinUnit)this).IsStealth)
        {
            combinedCritChance += 0.2f; // Stealth mode increases crit chance (for assassin unit)
        }

        bool _isCritHit = combinedCritChance > HitChance.NextDouble();

        int weaponDamage = EquippedWeapon.Use(this, target, isCritHit: _isCritHit);
        int totalDamage = weaponDamage + RollDamage();

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
            if (DefenseRating.Roll() > 6)
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
   

    public bool IsAlive()
    {
        return HP > 0;
    }


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
        int _hitMod = 0;
        int _defenseMod = 0;
        float _evasionChance = 0;
        int _range = 0;

        // check is dis ranged?
        bool isRangedUnit = this is RangedUnit;
        RangedUnit? unit = null;
        if (isRangedUnit) unit = (RangedUnit)this;

        Console.ForegroundColor = ConsoleColor.Green;

        switch (effect)
        {
            case Weather.Sunny:

                _hitMod = 3;
                _defenseMod = 2;

                if (isRangedUnit)
                {
                    _range = unit.Range;
                    unit.Range += 50;
                }

            break;

            case Weather.Cloudy:

                _hitMod = -1;
                _defenseMod = -1;
                _evasionChance = 0.10f;

                if (isRangedUnit)
                {
                    _range = unit.Range;
                    unit.Range -= 10;
                }

            break;

            case Weather.Rainy:

                _hitMod = -2;
                _defenseMod = -1;
                _evasionChance = 0.05f;

                if (isRangedUnit)
                {
                    _range = unit.Range;
                    unit.Range -= 30;
                }

            break;

            case Weather.Snowy:

                _hitMod = -1;
                _defenseMod = -2;
                _evasionChance = -0.10f;

                if (isRangedUnit)
                {
                    _range = unit.Range;
                    unit.Range -= 50;
                }

            break;

            case Weather.Windy:

                _hitMod = -1;
                _defenseMod = -1;
                _evasionChance = 0.10f;

                if (isRangedUnit)
                {
                    _range = unit.Range;
                    unit.Range -= 40;
                }

            break;

            case Weather.Foggy:

                _hitMod = -2;
                _defenseMod = 1;
                _evasionChance = 0.20f;

                if (isRangedUnit)
                {
                    _range = unit.Range;
                    unit.Range -= 80;
                }

            break;

            default:
            break;
        }

        // Apply weather changes
        HitChance.ModifyModifier(_hitMod);
        DefenseRating.ModifyModifier(_defenseMod);
        EvasionChance += _evasionChance;

        // -- Weather changes to undo
        // All units
        revertList.Push(() => HitChance.ModifyModifier(-_hitMod));
        revertList.Push(() => DefenseRating.ModifyModifier(-_defenseMod));
        revertList.Push(() => EvasionChance -= _evasionChance);

        // RangedUnit only
        if (isRangedUnit) revertList.Push(() => unit.Range = _range);

        //ApplySpecificWeatherEffect(effect); // for (subclasses w unique properties (was for (Range) unused)
    }


    public int RollDamage() => Damage.Roll();

    public int RollHitChance() => HitChance.Roll();

    public int RollDefenseRating() => DefenseRating.Roll();
}
