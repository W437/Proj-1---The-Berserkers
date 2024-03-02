﻿// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public abstract class RangedUnit : Unit
{
    public int Range { get; set; }
    protected RangedUnit(Dice damage, Dice hitChance, Dice defense, int hp, int armor, int range)
        : base(damage, hitChance, defense, hp, armor)
    {
        Range = range;
    }

/*    protected override void ApplySpecificWeatherEffect(Weather effect)
    {
        float originalRange = Range;

        switch (effect)
        {
            case Weather.Sunny:
                Range += 50f;
            break;

            case Weather.Cloudy:
                Range -= 10;
            break;

            case Weather.Snowy:
                Range -= 50;
            break;

            case Weather.Windy:
                Range -= 40;
            break;

            case Weather.Foggy:
                Range -= 80;
            break;

            default:   
            break;
        }

        revertList.Push(() => Range = originalRange);
    }*/
}
