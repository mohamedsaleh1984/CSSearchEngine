using CSSearchEngine.Abstraction;
using CSSearchEngine.Extensions;
using CSSearchEngine.Model;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSSearchEngine.Impl
{
    public class SearchPrompt : ISearchPrompt
    {
        string strIndexDir = "D:\\text-search-space\\_index";

        private static Tokenizer tokenizer = new Tokenizer();
        public List<SearchResultSet> GetResult(string Prompt)
        {
            List<string> Tokenized = tokenizer.GetTokens(Prompt);
            return GetSearchResultSets(Tokenized);
        }

        /// <summary>
        /// Load Index files
        /// </summary>
        /// <returns>List of Indexed Files</returns>
        private List<FileIndex> LoadIndexingDirectory()
        {
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
        /// Get Results
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private List<SearchResultSet> GetSearchResultSets(List<string>  tokens) {
            List<FileIndex> indexedFiles = LoadIndexingDirectory();
            List<SearchResultSet> searchResults = new();

            //all Tokens (Exact Match)
            var all_tokens = indexedFiles.Where(x => DocumentHasAllTokens(x,tokens)).ToList();

            //any Tokens
            var any_tokens = indexedFiles.Where(x => DocumentHasAnyTokens(x, tokens)).ToList();


            //merged + Sort

            return searchResults;
        }

        /// <summary>
        /// Check if search tokens are all exists in FileIndex
        /// </summary>
        /// <param name="fileIndex"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private bool DocumentHasAllTokens(FileIndex fileIndex,List<string> tokens)
        {
            bool re = fileIndex.Tokens.ContainsAll(tokens);
            return re;
        }

        /// <summary>
        /// Check if search any tokens exists in FileIndex
        /// </summary>
        /// <param name="fileIndex"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private bool DocumentHasAnyTokens(FileIndex fileIndex, List<string> tokens)
        {
            bool re = !fileIndex.Tokens.ContainsAny(tokens);
            return re;
        }
    }
}
