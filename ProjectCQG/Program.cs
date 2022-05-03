
namespace ProjectCQG
{
    class Program
    {
        private static string _directory = @"D:\";
        private static string PathInput { get; } = $"{_directory}Input.txt";
        private static string PathOutput { get; } = $"{_directory}OutPut.txt";
        private static string PathDictionary { get; } = $"{_directory}dictionary.txt";
        private static string PathWrongWords { get; } = $"{_directory}wrongwords.txt";
        static void Main(string[] args)
        {
            var spelling = new Spelling(PathDictionary);
            string[] word = { "hte", "rame", "in", "pain", "fells", "mainy", "oon", "teh", "lain", "was", "hints", "pliant" };
            for (int i = 0; i < word.Length; i++)
            {
                var correctWordArrey = spelling.Correct(word[i]);
                if (correctWordArrey.Length > 1)
                {
                    string aggregateString = correctWordArrey.Aggregate("{", (first, next) => $"{first} {next}") + " }";
                    Console.WriteLine($"{word[i]} => {aggregateString}");
                }
                Console.WriteLine($"{word[i]} => {correctWordArrey[0]}");
            }
        }
    }

}
