using CSSearchEngine.Abstraction;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Impl
{
	public class IndexingEngine : IIndexingEngine
	{
		private const int TopNTerms = 100;
		public Dictionary<string, int> CreateFrequenceTable(string filePath)
		{
			Dictionary<string, int> freqTable = new();
			List<string> tokenz = GetTokens(filePath);
			foreach (var term in tokenz)
			{
				if (freqTable.ContainsKey(term))
					freqTable[term]++;
				else
					freqTable.Add(term, 1);
			}


			return freqTable;
		}
		public List<string> GetTokens(string filePath)
		{
			string strFileContent = File.ReadAllText(filePath);
			return ExtractText(strFileContent).Split(" ",
								StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
								.Select(x => x.ToUpper())
								.Where(TokenCriterias())
								.ToList();
		}
		public Func<string, bool> TokenCriterias()
		{
			Func<string, bool> criList = x => x.Length > 3;
			return criList;
		}

		[Obsolete]
		public string ExtractText(string html)
		{
			if (html == null)
				throw new ArgumentNullException("ExtractText::html parameter.");

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
			return string.Join(" ", chunks);
		}

		public Dictionary<string, int> SortFrequencyTableByFrequency(Dictionary<string, int> FrequencyTable)
		{
			Dictionary<string, int> freqTable = new Dictionary<string, int>();
			List<KeyValuePair<string, int>> list = FrequencyTable.Take(TopNTerms).ToList();
			list.Sort(new KeyValueComparer());
			foreach (var item in list)
				freqTable.Add(item.Key, item.Value);
			return freqTable;
		}
		public static Func<string, string> TokenSelectors()
		{
			Func<string, string> criList = x => x.ToUpper();
			return criList;
		}
	}
}
