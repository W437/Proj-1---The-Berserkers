// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------

public abstract class Unit
{
    public virtual int Damage { get; set; }
    public virtual int HP { get; set; }
    public virtual int Armor { get; set; } // armor mitigates damage
    public float CritChance { get; set; }
    public float CritMultiplier { get; set; }
    public virtual float EvasionChance { get; set; } = 0.15f;

    protected Unit(int damage, int hp, int armor)
    {
        Damage = damage;
        HP = hp;
        Armor = armor;
    }

    public abstract void Attack(Unit target);
    public abstract void Defend(Unit attacker, int damangeAmount);

    public abstract void ReceiveDamage(int amount);

    public virtual void ReceiveHealing(int amount) { HP += amount; }

    public enum Race
    {
        Dwarf,
        Troll,
        Human,
    }

    public Race UnitRace { get; set; }
}
