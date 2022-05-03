using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCQG
{
    public class Data
    {
        public Data(string inputPath, string dictionaryPath, string wrongWordsPath)
        {
            ReadAndWrite(inputPath, dictionaryPath, wrongWordsPath);
        }

        private void ReadAndWrite(string path, string dictionaryPath, string wrongWordsPath)
        {
            try
            {
                var dictionaryLinesList = new List<string>();
                var wrongWordsList = new List<string>();
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null && line != "===")
                    {
                        dictionaryLinesList.Add(line);
                    }
                    WriteToFile(dictionaryPath, dictionaryLinesList);
                    dictionaryLinesList = null;
                    while ((line = sr.ReadLine()) != null && line != "===")
                    {
                        wrongWordsList.Add(line);
                    }
                    WriteToFile(wrongWordsPath, wrongWordsList);
                    wrongWordsList = null;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException + "\n" + ex.Message + "\n" + ex.TargetSite);
            }


        }
        public void WriteToFile(string path, List<string> dataList)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.Default))
                {
                    foreach (var item in dataList)
                    {
                        writer.WriteLine(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException + "\n" + ex.Message + "\n" + ex.TargetSite);
            }
        }
    }
}
