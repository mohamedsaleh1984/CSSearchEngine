namespace CSSearchEngine.Abstraction
{
    public interface IIndexingEngine
	{
        Dictionary<string, int> CreateFrequenceTable(string filePath);
        Dictionary<string, int> SortFrequencyTableByFrequency(Dictionary<string, int> FrequencyTable);
        List<string> GetTokens(string filePath);
    }
}
