using System;
using System.Collections.Generic;

public class Conference : IComparable<Conference>
{
    public string GroupName { get; set; } = "";
    public string StartDate { get; set; } = ""; // keeping as string per prompt
    public int Attendees { get; set; }

    // Sort by attendance ascending
    public int CompareTo(Conference? other)
    {
        if (other is null) return 1;
        return Attendees.CompareTo(other.Attendees);
    }

    public override string ToString()
        => $"{GroupName,-20} | {StartDate,-12} | {Attendees,6} attendees";
}

public class ConferencesDemo
{
    public static void Main()
    {
        var list = new List<Conference>();
        Console.WriteLine("Enter 5 conferences (group name, start date string, attendees):");

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"\nConference #{i+1}");
            Console.Write("  Group name: ");
            string g = Console.ReadLine() ?? "";

            Console.Write("  Start date (string): ");
            string d = Console.ReadLine() ?? "";

            Console.Write("  Attendees (int): ");
            int a = ReadInt();

            list.Add(new Conference { GroupName = g, StartDate = d, Attendees = a });
        }

        list.Sort(); // uses IComparable (attendance ascending)

        Console.WriteLine("\nSorted by attendance (smallest → largest):");
        Console.WriteLine(new string('-', 48));
        foreach (var c in list) Console.WriteLine(c);
    }

    private static int ReadInt()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int v) && v >= 0) return v;
            Console.Write("    Enter a nonnegative integer: ");
        }
    }
}
EOF
