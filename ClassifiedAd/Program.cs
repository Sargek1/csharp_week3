using System;

public class ClassifiedAd
{
    private string _category = "";
    private int _wordCount;
    private decimal _price; // computed: $0.09 per word

    public string Category
    {
        get => _category;
        set => _category = value ?? "";
    }

    public int WordCount
    {
        get => _wordCount;
        set
        {
            _wordCount = Math.Max(0, value);
            _price = ComputePrice(_wordCount);
        }
    }

    public decimal Price => _price;

    private static decimal ComputePrice(int words) => Math.Round(0.09m * words, 2);
}

public class TestClassifiedAd
{
    public static void Main()
    {
        var ad1 = new ClassifiedAd { Category = "Used Cars", WordCount = 44 };
        var ad2 = new ClassifiedAd { Category = "Appliances", WordCount = 12 };

        Display(ad1);
        Display(ad2);
    }

    private static void Display(ClassifiedAd ad)
    {
        Console.WriteLine($"Category: {ad.Category}");
        Console.WriteLine($"Words:    {ad.WordCount}");
        Console.WriteLine($"Price:    {ad.Price:C}");
        Console.WriteLine(new string('-', 30));
    }
}
EOF
