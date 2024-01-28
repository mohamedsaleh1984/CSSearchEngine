namespace CSSearchEngine.Abstraction
{
    public interface IIndexingEngine
	{
        string CreateSearchableFile(string originalFile);
        Dictionary<string, int> CreateFrequenceTable(string filePath);
        Dictionary<string, int> SortFrequencyTableByFrequency(Dictionary<string, int> FrequencyTable);
        List<string> GetTokens(string filePath);
        void StartIndexing(string strContent);
    }
}
