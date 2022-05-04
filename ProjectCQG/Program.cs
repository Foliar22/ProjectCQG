using System.Text;
using System.Text.RegularExpressions;

namespace ProjectCQG
{
    class Program
    {
        private static string _directory = @"D:\";
        private static string PathInput { get; } = $"{_directory}Input.txt";
        private static string PathOutput { get; } = $"{_directory}OutPut.txt";
        private static string PathDictionary { get; } = $"{_directory}dictionary.txt";
        private static string PathWrongWords { get; } = $"{_directory}wrongwords.txt";

        private static Regex _wordRegex = new Regex("[a-z]+", RegexOptions.Compiled);
        static void Main(string[] args)
        {
            var data = new Data(PathInput, PathDictionary, PathWrongWords);
            List<string> wrongWordsList = new List<string>();
            using (StreamReader sr = new StreamReader(PathWrongWords, Encoding.Default))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    wrongWordsList.Add(line);
                }
            }
            var correctList = Correction(wrongWordsList);
            data.WriteToFile(PathOutput, correctList);
            Console.WriteLine("Done!");


            //var spelling = new Spelling(PathDictionary);
            //string[] word = { "hte", "rame", "in", "pain", "fells", "mainy", "oon", "teh", "lain", "was", "hints", "pliant" };
            //for (int i = 0; i < word.Length; i++)
            //{
            //    var correctWordArrey = spelling.GetCorrectWords(word[i]);
            //    if (correctWordArrey.Length > 1)
            //    {
            //        string aggregateString = correctWordArrey.Aggregate("{", (first, next) => $"{first} {next}") + " }";
            //        Console.WriteLine($"{word[i]} => {aggregateString}");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{word[i]} => {correctWordArrey[0]}");
            //    }

            //}
        }

        private static List<string> Correction(List<string> wrongWordsList)
        {
            var spelling = new Spelling(PathDictionary);
            List<string> correctList = new List<string>();
            foreach (string line in wrongWordsList)
            {
                string correctWords = "";
                string[] words = line.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (_wordRegex.IsMatch(words[i]))
                    {
                        var correctWordArrey = spelling.GetCorrectWords(words[i]);
                        if (correctWordArrey.Length > 1)
                        {
                            words[i] = "{" + correctWordArrey.Aggregate((first, next) => $"{first} {next}") + "} ";
                        }
                        else
                        {
                            words[i] = correctWordArrey[0] + " ";
                        }
                    }
                    else
                    {
                        words[i] += " ";
                    }

                    correctWords += words[i];
                }
                correctList.Add(correctWords);
            }
            return correctList;

        }
    }

}
