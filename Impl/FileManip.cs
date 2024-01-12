using CSSearchEngine.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Impl
{
	public class FileManip : IFileManip
    {
		//TODO
		private List<string> GetAcceptedFilesExtensions()
		{
			return new List<string>()
			{
				"html","html"
			};
		}

		public List<string> GetHtmlFilesPaths(string location)
		{
			if(!Directory.Exists(location)) {
				throw new Exception("Directory is not exists.");
			}

			return Directory.GetFiles(location, "*.*", SearchOption.AllDirectories)
						  .Where(x => (x.ToLower().EndsWith("html") || x.ToLower().EndsWith("html")))
						  .ToList();
		}
	}
}
