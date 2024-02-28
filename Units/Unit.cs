// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public abstract class Unit
{
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


    protected Unit(Dice damage, int hp, int armor)
    {
        Damage = damage;
        HP = hp;
        Armor = armor;
    }


    public void Attack(Unit target)
    {
        if (!IsAlive()) return;

        Console.Write($"{GetType().Name} attacks ");

        if (HitChance.Roll() <= 4)
        {
            Console.WriteLine("and misses.");
            return;
        }

        int weaponDamage = EquippedWeapon.Use(this, target);
        int totalDamage = weaponDamage + RollDamage();

        if (Random.Shared.NextDouble() < CritChance)
        {
            totalDamage = (int)(totalDamage * CritMultiplier);
            Console.WriteLine($"and lands a critical hit for {totalDamage} damage!");
        }
        else
        {
            Console.WriteLine($"and hits for {totalDamage} damage.");
        }

        // Target defends against this attack.
        target.Defend(this, totalDamage);
    }
    

    public virtual void Defend(Unit attacker, int damageAmount)
    {
        if (IsAlive())
        {
            // General defense mechanism for all units - if (defense or stealth mode)
            if (RollDefenseRating() > 6 || (this is AssassinUnit && ((AssassinUnit)this).IsStealth))
            {
                Console.WriteLine($" => {GetType().Name} gracefully avoids the attack!\n");
                if (this is AssassinUnit)
                {
                    ((AssassinUnit)this).IsStealth = false; // exit stealth mode after avoiding the attack
                }
            }
            else
            {
                // Handling damage
                int finalDamage;
                if (this is AssassinUnit && ((AssassinUnit)this).IsStealth)
                {
                    // Reduce damage taken by half while in stealth for AssassinUnit
                    int damageReductionInStealth = Armor / 2;
                    finalDamage = Math.Max(0, damageAmount - damageReductionInStealth);
                    ((AssassinUnit)this).IsStealth = false; // exit stealth mode after taking damage
                }
                else
                {
                    // Standard damage reduction for all units
                    finalDamage = Math.Max(0, damageAmount - Armor);
                }

                TakeDamage(finalDamage);
                Console.WriteLine($"{GetType().Name} receives {finalDamage} damage.");
            }
        }
    }


    protected virtual void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Console.WriteLine($"{GetType().Name} has been defeated!");
        }
        else
        {
            Console.WriteLine($"{GetType().Name}'s remaining HP: {HP}");
        }
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

    protected virtual void ApplySpecificWeatherEffect(Weather effect) { }

    public virtual void ApplyWeather(Weather effect)
    {
        int _hitMod = 0;
        int _defenseMod = 0;
        float _evasionChance = 0;
        float _range = 0;

        // check is dis ranged?
        bool isRangedUnit = this is RangedUnit;
        RangedUnit? unit = null;
        if (isRangedUnit) unit = (RangedUnit)this; 

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

                HitChance.ModifyModifier(_hitMod);
                DefenseRating.ModifyModifier(_defenseMod);
                //Console.WriteLine("Sunny weather improves hit chance and range.");
  
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

                HitChance.ModifyModifier(_hitMod);
                DefenseRating.ModifyModifier(_defenseMod);
                EvasionChance += _evasionChance;
                //Console.WriteLine("Cloudy weather reduces evasion chance.");

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

                HitChance.ModifyModifier(_hitMod);
                DefenseRating.ModifyModifier(_defenseMod);
                EvasionChance += _evasionChance;
                //Console.WriteLine("Rainy weather decreases defense rating.");

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

                HitChance.ModifyModifier(_hitMod);
                DefenseRating.ModifyModifier(_defenseMod);
                EvasionChance += _evasionChance;
                //Console.WriteLine("Snowy weather significantly decreases defense rating.");

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

                HitChance.ModifyModifier(_hitMod);
                DefenseRating.ModifyModifier(_defenseMod);
                EvasionChance += _evasionChance;
                //Console.WriteLine("Windy weather reduces range.");

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

                HitChance.ModifyModifier(_hitMod);
                DefenseRating.ModifyModifier(_defenseMod);
                EvasionChance += _evasionChance;
                //Console.WriteLine("Foggy weather greatly reduces range and increases evasion chance.");

            break;

            default:
            break;
        }

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
