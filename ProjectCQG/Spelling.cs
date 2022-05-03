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
		public string[] Correct(string word)
		{
			string[] result;
			if (string.IsNullOrEmpty(word))
			{
				result = new string[] { word };
				return result;
			}
			word = word.ToLower();
			//Know
			if (_dictionary.ContainsKey(word))
			{
				result = new string[] { word };
				return result;
			}
			return result = new string[] {" "};
		}
			private List<Tuple<string, string>> Splits(string word)
		{
			var splits = new List<Tuple<string, string>>();
			for (int i = 0; i < word.Length; i++)
			{
				var tuple = new Tuple<string, string>(word.Substring(0, i), word.Substring(i));
				splits.Add(tuple);
			}
			return splits;
		}
		private List<string> Inserts(List<Tuple<string, string>> splits)
		{
			var inserts = new List<string>();
			for (int i = 0; i < splits.Count; i++)
			{
				string a = splits[i].Item1;
				string b = splits[i].Item2;
				for (char c = 'a'; c <= 'z'; c++)
				{
					inserts.Add(a + c + b);
				}
			}
			return inserts;
		}
		private List<string> Deletes(List<Tuple<string, string>> splits)
		{
			var deletes = new List<string>();
			for (int i = 0; i < splits.Count; i++)
			{
				string a = splits[i].Item1;
				string b = splits[i].Item2;
				if (!string.IsNullOrEmpty(b))
				{
					deletes.Add(a + b.Substring(1));
				}
			}
			return deletes;
		}


	}
}
