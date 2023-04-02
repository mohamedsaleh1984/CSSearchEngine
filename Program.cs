using HtmlAgilityPack;
using System.Text.Json;

namespace CSSearchEngine
{
    public class Program
    {
        private const string strDirectory = "D:\\HTML-REZ";
        private const int TopNTerms = 100;
        static void Main(string[] args)
        {
            List<string> filesList = GetHtmlFilesPaths();
            Dictionary<string, Dictionary<string, int>> DocsFT = new();

            foreach (var filePath in filesList)
            {
                string strFileName = Path.GetFileNameWithoutExtension(filePath);
                var ft = CreateFrequenceTable(filePath);
                var sft = SortFrequencyTableByFrequency(ft);
                
                if (sft.Count() == 0)
                    continue;

                Console.WriteLine($"{strFileName}");
                foreach (KeyValuePair<string, int> kvp in sft)
                    Console.WriteLine($"\t{kvp.Key}: {kvp.Value}");
                Console.WriteLine("");

                Console.WriteLine($"Indexing \"{filePath}\" has {sft.Values.Sum()} tokens");
                DocsFT.Add(strFileName, sft);

            }


            //Write Frequency to JSON file
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<Dictionary<string, Dictionary<string, int>>>(DocsFT, opt);
            File.WriteAllText("index.json", strJson);
        }
        public static List<string> GetHtmlFilesPaths()
        {
           return  Directory.GetFiles(strDirectory, "*.*", SearchOption.AllDirectories)
                         .Where(x => (x.ToLower().EndsWith("html") || x.ToLower().EndsWith("htm")))
                         .ToList();
        }
        public static List<string> GetTokens(string strFilePath)
        {
            string strFileContent = File.ReadAllText(strFilePath);
            return  ExtractText(strFileContent).Split(" ",
                                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.ToUpper())
                                .Where(TokenCriterias())
                                .ToList();
        }
        /// <summary>
        /// Define Token Rules/Criteria
        /// </summary>
        /// <returns></returns>
        public static Func<string,bool> TokenCriterias()
        {
            Func<string, bool> criList = x => x.Length > 3;
            return criList;
        }
        public static Func<string, string> TokenSelectors()
        {
            Func<string, string> criList = x => x.ToUpper();
            return criList;
        }
        public static Dictionary<string,int> CreateFrequenceTable(string strFilePath)
        {
            Dictionary<string, int> freqTable = new  Dictionary<string, int>();
            List<string> tokenz = GetTokens(strFilePath);
            foreach (var term in tokenz)
            {
                if (freqTable.ContainsKey(term))
                    freqTable[term]++;
                else
                    freqTable.Add(term, 1);                
            }


            return freqTable;
        }
        public static Dictionary<string, int> SortFrequencyTableByFrequency(Dictionary<string, int> FrequencyTable)
        {
            Dictionary<string, int> freqTable = new Dictionary<string, int>();
            List<KeyValuePair<string, int>> list = FrequencyTable.Take(TopNTerms).ToList();
            list.Sort(new  KeyValueComparer());
            foreach (var item in list)
            {
                freqTable.Add(item.Key, item.Value);
            }
            return freqTable;
        }
        public static string ExtractText(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var chunks = new List<string>();

            foreach (var item in doc.DocumentNode.DescendantNodesAndSelf())
            {
                if (item.NodeType == HtmlNodeType.Text)
                {
                    if (item.InnerText.Trim() != "")
                    {
                        chunks.Add(item.InnerText.Trim());
                    }
                }
            }
            return String.Join(" ", chunks);
        }
    }
}