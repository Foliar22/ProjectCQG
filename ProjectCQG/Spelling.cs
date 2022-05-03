using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectCQG
{
   public class Spelling
    {
		private Dictionary<string, int> _dictionary = new Dictionary<string, int>();
		private static Regex _wordRegex = new Regex("[a-z]+", RegexOptions.Compiled);

		public Spelling()
		{
			string fileContent = File.ReadAllText(@"D:\dis2.txt");
			//List<string> wordList = fileContent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
			List<string> wordList = fileContent.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
			int i = 0;
			foreach (var word in wordList)
			{
				string trimmedWord = word.Trim().ToLower();
				if (_wordRegex.IsMatch(trimmedWord))
				{
					if (!_dictionary.ContainsKey(trimmedWord))
					{
						_dictionary.Add(trimmedWord, i);
					}

				}
				i++;
			}
		}
	}
}
