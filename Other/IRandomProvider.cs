// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public interface IRandomProvider
{
    int Next(int minValue, int maxValue);
    double NextDouble();
}