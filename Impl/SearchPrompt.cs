using CSSearchEngine.Abstraction;
using CSSearchEngine.Extensions;
using CSSearchEngine.Model;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSSearchEngine.Impl
{
    public class SearchPrompt : ISearchPrompt
    {
        public List<SearchResultSet> GetResult(string Prompt)
        {
            List<string> Tokenized = Tokenize(Prompt);
            return GetSearchResultSets(Tokenized);
        }

        /// <summary>
        /// Load Index files
        /// </summary>
        /// <returns>List of Indexed Files</returns>
        private List<FileIndex> LoadIndexingDirectory()
        {
            string strIndexDir = "D:\\text-search-space\\_index";

            List<string> indexedFiles = Directory.GetFiles(strIndexDir).ToList();

            List<FileIndex> indexedJson = new();
            foreach (string file in indexedFiles)
            {
                string jsonString = File.ReadAllText(file);
                FileIndex? _tmpFileIndex = JsonSerializer.Deserialize<FileIndex>(jsonString);

                if (_tmpFileIndex != null)
                    indexedJson.Add(_tmpFileIndex);
            }

            return indexedJson;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Prompt"> Tolulope Case Manager </param>
        /// <returns>
        ///  TOLULOPE
        ///  CASE
        ///  MANAGER
        /// </returns>
        private List<string> Tokenize(string Prompt)
        {
            return Prompt.Split(new char[] { ' '}).Select(x => x.ToUpper()).ToList();
        }


        private List<SearchResultSet> GetSearchResultSets(List<string>  tokens) {
            List<FileIndex> indexedFiles = LoadIndexingDirectory();
            List<SearchResultSet> searchResults = new();

            //all Tokens
            var all_tokens = indexedFiles.Where(x => DocumentHasAllTokens(x,tokens)).ToList();

            //any Tokens
            var any_tokens = indexedFiles.Where(x => DocumentHasAnyTokens(x, tokens)).ToList();


            //merged + Sort

            return searchResults;
        }

        private bool DocumentHasAllTokens(FileIndex fileIndex,List<string> tokens)
        {
            bool re = fileIndex.Tokens.ContainsAll(tokens);
            return re;
        }
        private bool DocumentHasAnyTokens(FileIndex fileIndex, List<string> tokens)
        {
            bool re = fileIndex.Tokens.ContainsAll(tokens);
            return re;
        }



    }
}
