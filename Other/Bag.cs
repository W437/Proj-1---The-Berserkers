// ---- C# II (Dor Ben Dor) ----
// Wael Abd Elal
// -----------------------------
public class Bag : IRandomProvider
{
    private List<int> numbers;
    private int index;

    public Bag(IEnumerable<int> sourceNumbers)
    {
        numbers = new List<int>(sourceNumbers);
        Shuffle();
    }

    public int Next(int minValue, int maxValue)
    {
        int result = numbers[index];
        index = (index + 1) % numbers.Count;
        return result;
    }

    public double NextDouble() { return 0; }

    private void Shuffle()
    {
        Random random = new Random();

        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        index = 0;
    }
}

/*class Program2
{
    static void Main()
    {
        Bag numberBag = new Bag(new List<int> { 1, 2, 3, 4, 5, 6, 7 });


        for (int i = 0; i < 20; i++)
        {
            int randomNumber = numberBag.Next(1, 5);
            Console.WriteLine($"Rand #: {randomNumber}");
        }
    }
}*/
