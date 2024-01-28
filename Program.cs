using CSSearchEngine.Abstraction;
using CSSearchEngine.Impl;
using HtmlAgilityPack;
using System.Text.Json;

namespace CSSearchEngine
{
    public class Program
    {
        private const string strDirectory = "D:\\text-search-space";
		private static IndexingEngine IndexEngine = new IndexingEngine();
        private static SearchPrompt SearchEngine = new SearchPrompt();

        static void Main(string[] args)
        {
            IndexEngine.StartIndexing(strDirectory);

            string query = "Hello, World";
            SearchEngine.GetResult(query);

        }
    }
}