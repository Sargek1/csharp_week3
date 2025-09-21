using System;
using System.Globalization;

public class Job
{
    private string _description = "";
    private double _hours;          // e.g., 3.5
    private decimal _ratePerHour;   // e.g., 25.00
    private decimal _totalFee;      // computed = hours * rate

    public string Description
    {
        get => _description;
        set => _description = value ?? "";
    }

    public double Hours
    {
        get => _hours;
        set
        {
            _hours = Math.Max(0, value);
            Recalc();
        }
    }

    public decimal RatePerHour
    {
        get => _ratePerHour;
        set
        {
            _ratePerHour = value < 0 ? 0 : value;
            Recalc();
        }
    }

    public decimal TotalFee => _totalFee;

    private void Recalc() => _totalFee = (decimal)_hours * _ratePerHour;

    // Combine two jobs:
    // - Description: "desc1 and desc2"
    // - Hours: sum
    // - Rate: WEIGHTED average by hours (fixes unfair simple average)
    public static Job operator +(Job a, Job b)
    {
        if (a is null || b is null) throw new ArgumentNullException();
        double hoursSum = a.Hours + b.Hours;

        decimal weightedRate;
        if (hoursSum <= 0)
        {
            weightedRate = (a.RatePerHour + b.RatePerHour) / 2m;
        }
        else
        {
            weightedRate = ((decimal)a.Hours * a.RatePerHour + (decimal)b.Hours * b.RatePerHour)
                           / (decimal)hoursSum;
        }

        return new Job
        {
            Description = $"{a.Description} and {b.Description}",
            Hours = hoursSum,
            RatePerHour = Math.Round(weightedRate, 2)
        };
    }

    public override string ToString()
        => $"{Description,-30} | Hours: {Hours,5:0.##} | Rate: {RatePerHour,7:C} | Fee: {TotalFee,8:C}";
}

public class DemoJobs
{
    public static void Main()
    {
        var paintHouse = new Job { Description = "Paint house", Hours = 10, RatePerHour = 100m };
        var dogWalk    = new Job { Description = "Dog walking", Hours = 1,  RatePerHour = 10m };
        var windows    = new Job { Description = "Wash windows", Hours = 3.5, RatePerHour = 25m };

        Console.WriteLine("Individual jobs:");
        Console.WriteLine(paintHouse);
        Console.WriteLine(dogWalk);
        Console.WriteLine(windows);
        Console.WriteLine(new string('-', 78));

        Console.WriteLine("Simple combination examples (weighted rate in + operator):");
        var combo1 = paintHouse + dogWalk;     // weighted rate
        Console.WriteLine(combo1);

        var combo2 = combo1 + windows;         // chaining still weighted by hours
        Console.WriteLine(combo2);
    }
}
EOF
cat > DemoJobs/DemoJobs.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
EOF
