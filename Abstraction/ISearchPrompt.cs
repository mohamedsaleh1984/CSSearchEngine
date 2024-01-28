using CSSearchEngine.Model;
namespace CSSearchEngine.Abstraction
{
    public interface ISearchPrompt
    {
        List<SearchResultSet> GetResult(string Prompt);
    }
}
