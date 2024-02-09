// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public abstract class Unit
{
    public virtual Dice Damage { get; set; }
    public virtual Dice HitChance { get; set; }
    public virtual Dice DefenseRating { get; set; }
    public virtual int HP { get; set; }
    public virtual int Armor { get; set; } // armor mitigates damage
    public virtual int CarryCapacity { get; set; }
    public float CritChance { get; set; }
    public float CritMultiplier { get; set; }
    public virtual float EvasionChance { get; set; } = 0.15f;

    protected Unit(Dice damage, int hp, int armor)
    {
        Damage = damage;
        HP = hp;
        Armor = armor;
    }

    public abstract void Attack(Unit target);
    public abstract void Defend(Unit attacker, int damageAmount);
    public abstract void ReceiveDamage(int amount);
    public virtual void ReceiveHealing(int amount) { HP += amount; }
    public abstract void WeatherEffect(WeatherEffects effect);
    public Race UnitRace { get; set; }

    public enum Race
    {
        Dwarf,
        Troll,
        Human,
    }

    public enum WeatherEffects
    {
        Sunny,
        Cloudy,
        Rainy,
        Snowy,
        Windy,
        Foggy
    }
}
