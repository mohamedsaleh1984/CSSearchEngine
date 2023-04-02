namespace CSSearchEngine
{
    public class Program
    {
        private const string strDirectory = "D:\\HTML-REZ";

        static void Main(string[] args)
        {
            List<string> filesList = GetHtmlFiles();
            Dictionary<string, Dictionary<string, ulong>> DocsFT 
                        = new Dictionary<string, Dictionary<string, ulong>>();

            foreach (var filePath in filesList)
            {
                string strFileName = Path.GetFileNameWithoutExtension(filePath);
                DocsFT.Add(strFileName, CreateFrequenceTable(filePath));
            }

        }

        public static List<string> GetHtmlFiles()
        {
            List<string> files =
                Directory.GetFiles(strDirectory, "*.*", SearchOption.AllDirectories)
                         .Where(x => (x.ToLower().EndsWith("html") || x.ToLower().EndsWith("htm")))
                         .ToList();
            return files;
        }

        public static List<string> GetTokens(string strFilePath)
        {
            return File.ReadAllText(strFilePath).Split(" ",
                                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.ToUpper())
                                .Where( x => x.Length > 1)
                                .ToList();
        }

        public static Dictionary<string,ulong> CreateFrequenceTable(string strFilePath)
        {
            Dictionary<string, ulong> freqTable = new  Dictionary<string, ulong>();
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
    }
}