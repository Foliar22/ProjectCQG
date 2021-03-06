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

		public Spelling(string pathDictionary)
		{
			try
			{
				string fileContent = File.ReadAllText(pathDictionary);
				char[] splits = new char[] { ' ', '\n', '\r' };
				//List<string> wordList = fileContent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
				List<string> wordList = fileContent.Split(splits, StringSplitOptions.RemoveEmptyEntries).ToList();
				int i = 0;
				foreach (var word in wordList)
				{
					string trimmedWord = word.Trim().ToLower();
					if (_wordRegex.IsMatch(trimmedWord))
					{
						if (!_dictionary.ContainsKey(trimmedWord))
						{
							_dictionary.Add(trimmedWord, i++);
						}

					}
				}
			}
            catch(Exception ex)
            {
				Console.WriteLine(ex.InnerException + "\n" + ex.Message + "\n" + ex.TargetSite);
			}
		}
		/// <summary>
		/// The GetCorrectWords method will return an array of possible correct words
		/// </summary>
		/// <returns>
		/// Array of strings
		/// </returns>
		public string[] GetCorrectWords(string word)
		{
			string[] inputWord = new string[] { word };
			string[] result;
			try
			{
				if (string.IsNullOrEmpty(word))
				{
					return inputWord;
				}
			}
			catch (Exception ex)
            {
				Console.WriteLine(ex.InnerException + "\n" + ex.Message + "\n" + ex.TargetSite);
				return inputWord;
			}
			word = word.ToLower();
			//Know
			if (_dictionary.ContainsKey(word))
			{
				return inputWord;
			}
			//Unknow
			List<Tuple<string, string>> listSplits = Splits(word);
			Dictionary<string, int> candidates = new Dictionary<string, int>();
			List<string> listInserts = Inserts(listSplits);
			List<string> listDeletes = Deletes(listSplits);
			candidates = CheckWordList(listInserts);
			foreach (var candidat in CheckWordList(listDeletes))
			{
				candidates.Add(candidat.Key, candidat.Value);
			}
			if (candidates.Count > 0)
			{
				return result = candidates.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
			}
			List<string> listInsertsAndDeletes = new List<string>();
			foreach (string wordWithInsert in listInserts)
			{
				var splitsInserts = Splits(wordWithInsert);
				listInsertsAndDeletes.AddRange(Deletes(splitsInserts));
			}
			candidates = CheckWordList(listInsertsAndDeletes);
			if (candidates.Count > 0)
			{
				return result = candidates.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
			}

			var resultWord = "{" + inputWord[0] + "?}";
			return result = new string[] { resultWord };

		}
		/// <summary>
		/// The CheckWordList method will check the list of word for matches with the dictionary
		/// </summary>
		/// <returns>
		/// Dictionary of correct words
		/// </returns>
		private Dictionary<string, int> CheckWordList(List<string> listCadidates)
		{
			Dictionary<string, int> result = new Dictionary<string, int>();
			foreach (string wordVariation in listCadidates)
			{
				if (_dictionary.ContainsKey(wordVariation) && !result.ContainsKey(wordVariation))
					result.Add(wordVariation, _dictionary[wordVariation]);
			}
			return result;
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
