string garble = AdventUtils.AdventIO.GetString("input3.txt");
int sumOfMults = Day3.MullItOver.SumOfMultiplications(garble);
int sumOfOptionalMults = Day3.MullItOver.SumOfOptionalMultiplications(garble);
Console.WriteLine($"Sum of all multiplications: {sumOfMults}");
Console.WriteLine($"Sum of optional multiplications: {sumOfOptionalMults}");
