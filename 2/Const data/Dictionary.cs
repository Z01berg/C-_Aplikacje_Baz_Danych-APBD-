namespace APBD2;

public class Dictionary
{
    public Dictionary<string, double> product_temperatures = new Dictionary<string, double>()
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausages", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 }
    };

    public void ShowDictionary()
    {
        int index = 1;
        foreach (var prod in product_temperatures)
        {
            Console.WriteLine($"{index}  ▌ {prod.Key} ▌ {prod.Value}");
            index++;
        }
    }

    public string GetNameProduct(int id)
    {
        int index = 1;
        foreach (var prod in product_temperatures)
        {
            if (id == index)
            {
                return prod.Key;
            }
            index++;
        }

        return null;
    }

    public double GetTempProduct(int id)
    {
        int index = 1;
        foreach (var prod in product_temperatures)
        {
            if (id == index)
            {
                return prod.Value;
            }
            index++;
        }

        return 0;
    }
}