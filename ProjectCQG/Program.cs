
namespace ProjectCQG
{
    class Program
    {
        static void Main(string[] args)
        {
            var spelling = new Spelling();
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
