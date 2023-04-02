namespace CSSearchEngine
{
    public class Program
    {
        private   const string strDirectory = "D:\\HTML-REZ";

        static void Main(string[] args)
        {
           
        }


        public List<string> GetHtmlFiles() {
            List<string> files = 
                Directory.GetFiles(strDirectory, "*.*", SearchOption.AllDirectories)
                         .Where(x => (x.ToLower().EndsWith("html") || x.ToLower().EndsWith("htm")))
                         .ToList();
            return files;
        }
    }
}