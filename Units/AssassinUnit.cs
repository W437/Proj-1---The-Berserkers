// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public abstract class AssassinUnit : Unit
{
    public virtual bool IsStealth { get; set; }

    protected AssassinUnit(Dice damage, Dice hitChance, Dice defense, int hp, int armor)
        : base(damage, hitChance, defense, hp, armor)
    {
        IsStealth = true;
    }

/*    public override void Defend(Unit attacker, int damageAmount)
    {
        if(IsAlive())
        {
            string unitName = GetType().Name;

            if (DefenseRating.Roll() > 6)
            {
                Console.WriteLine($" => {unitName} gracefully avoids the attack!\n");
            }

            else
            {
                int finalDamage;
                if (IsStealth)
                {
                    // Reduce damage taken by half while in stealth
                    int damageReductionInStealth = Armor / 2;
                    finalDamage = Math.Max(0, damageAmount - damageReductionInStealth);
                    IsStealth = false;
                }
                else
                {
                    int damageReduction = Armor;
                    finalDamage = Math.Max(0, damageAmount - damageReduction);
                }

                TakeDamage(finalDamage);
            }
        }
    }*/
}
