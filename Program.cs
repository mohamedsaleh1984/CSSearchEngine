using CSSearchEngine.Abstraction;
using CSSearchEngine.Impl;
using HtmlAgilityPack;
using System.Text.Json;

namespace CSSearchEngine
{
    public class Program
    {
        private const string strDirectory = "D:\\HTML-REZ";
      
        private static FileManip fileManip = new FileManip();
		private static IndexingEngine engine = new IndexingEngine();
		static void Main(string[] args)
        {
			List<string> filesList = fileManip.GetHtmlFilesPaths(strDirectory);
            Dictionary<string, Dictionary<string, int>> docsFreqTable = new();

            foreach (var filePath in filesList)
            {
                string strFileName = Path.GetFileNameWithoutExtension(filePath);
                var ft = engine.CreateFrequenceTable(filePath);
                var sft = engine.SortFrequencyTableByFrequency(ft);

                if (!sft.Any())
                    continue;

                Console.WriteLine($"{strFileName}");
                foreach (KeyValuePair<string, int> kvp in sft)
                    Console.WriteLine($"\t{kvp.Key}: {kvp.Value}");
                Console.WriteLine("");

                Console.WriteLine($"Indexing \"{filePath}\" has {sft.Values.Sum()} tokens");
                docsFreqTable.Add(strFileName, sft);

            }


            //Write Frequency to JSON file
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<Dictionary<string, Dictionary<string, int>>>(docsFreqTable, opt);
            File.WriteAllText("index.json", strJson);
        }
        

        public static Func<string, string> TokenSelectors()
        {
            Func<string, string> criList = x => x.ToUpper();
            return criList;
        }
       
       
       
    }
}