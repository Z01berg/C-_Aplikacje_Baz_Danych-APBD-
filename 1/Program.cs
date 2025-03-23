class Program
{
    public static void Main(string[] args)
    {
        int[] numbers = new[] { 1, 2, 3, 55, 56, 78 };
        Console.WriteLine($"AVERAGE: {feature_avg(numbers)}\n");
        Console.WriteLine($"MAX: {feature_max(numbers)}\n");
    }
    
    public static int feature_avg(int[] numbers)
    {
        int SUM = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            SUM += numbers[i];
        }

        int avg = SUM / numbers.Length;
    
        return avg;
    }
    
    public static int feature_max(int[] numbers)
    {
        int max = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] > max)
            {
                max = numbers[i];
            }
        }
        
        return max;
    }
}