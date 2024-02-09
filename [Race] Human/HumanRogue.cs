// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public sealed class HumanRogue : AssassinUnit
{
    public HumanRogue() : base(damage: new Dice(2, 7, 3), hp: 110, armor: 50)
    {
        UnitRace = Race.Human;
        CritChance = 0.15f;
        CritMultiplier = 1.5f;
        EvasionChance += 0.1f;
        CarryCapacity = 50;
        CurrentWeapon = CreateWeapon(typeof(VenomStinger));
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
            case nameof(VenomStinger):
                return new VenomStinger();
            default:
                return null;
        }
    }

    public sealed class VenomStinger : AssassinWeapon
    {
        public VenomStinger() : base(baseDamage: 30, additionalCritChance: 0.15f, critMultiplierBoost: 0.5f) { }

        public override void UseWeapon(Unit attacker, Unit target, bool isStealthMode, int extraDamage = 0)
        {
            Console.WriteLine("Human Rogue performs a Swift Attack with the Venom Stinger!");

            Random random = new Random();
            int randomValue = random.Next(1, 101);

            if (randomValue <= 20) // 20%
            {
                int swiftAttackDamage = Damage * 2; // 2x damage in swift attack
                base.UseWeapon(attacker, target, isStealthMode, swiftAttackDamage);
                Console.WriteLine("Swift Attack! Damage X2!");
            }
            else
            {
                base.UseWeapon(attacker, target, isStealthMode);
                Console.WriteLine("Normal Attack.");
            }
        }
    }
}
