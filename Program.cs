using CSSearchEngine.Abstraction;
using CSSearchEngine.Impl;
using HtmlAgilityPack;
using System.Text.Json;

namespace CSSearchEngine
{
    public class Program
    {
        private const string strDirectory = "D:\\text-search-space";
		private static IndexingEngine engine = new IndexingEngine();

		static void Main(string[] args)
        {
            engine.StartIndexing(strDirectory);
        }
    }
}