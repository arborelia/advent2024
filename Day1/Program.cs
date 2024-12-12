var pairs = Day1.NumberListOperations.PairsFromInput("input1a.txt");
int diff = Day1.NumberListOperations.SortedDifference(pairs);
int sim = Day1.NumberListOperations.SimilarityScore(pairs);
Console.WriteLine($"Sorted difference: {diff}");
Console.WriteLine($"Similarity score: {sim}");
