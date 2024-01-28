using CSSearchEngine.Abstraction;
using CSSearchEngine.Extensions;

namespace CSSearchEngine.Impl
{
	public class FileManip : IFileManip
    {
		private List<string> GetAcceptedFilesExtensions()
		{
			return new List<string>()
			{
				"txt"
			};
		}

		public List<string> GetFilesPaths(string location)
		{
			if(!Directory.Exists(location)) {
				throw new Exception("Directory is not exists.");
			}

			string[] accptedExtensions = GetAcceptedFilesExtensions().ToArray();

            return Directory.GetFiles(location, "*.*", SearchOption.AllDirectories)
						  .Where(x => x.EndsWith(accptedExtensions))
						  .ToList();
		}
	}
}
