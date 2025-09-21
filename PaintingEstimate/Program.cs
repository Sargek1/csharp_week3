using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter room length (feet): ");
        double length = ReadDouble();

        Console.Write("Enter room width (feet): ");
        double width = ReadDouble();

        double price = CalculatePaintCost(length, width);
        Console.WriteLine($"\nEstimated painting cost: {price:C}");
    }

    static double CalculatePaintCost(double length, double width)
    {
        const double ceilingHeight = 9.0;   // feet
        const double pricePerSqFt = 6.0;    // dollars
        double wallArea = 2 * (length + width) * ceilingHeight; // 4 walls
        return wallArea * pricePerSqFt;
    }

    static double ReadDouble()
    {
        while (true)
        {
            var s = Console.ReadLine();
            if (double.TryParse(s, out double val) && val >= 0) return val;
            Console.Write("Please enter a nonnegative number: ");
        }
    }
}
EOF
