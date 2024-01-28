namespace CSSearchEngine.Abstraction
{
    public interface IIndexingEngine
	{
        string CreateSearchableFile(string originalFile);
        Dictionary<string, int> CreateFrequenceTable(string filePath);
        Dictionary<string, int> SortFrequencyTableByFrequency(Dictionary<string, int> FrequencyTable);
        void StartIndexing(string strContent);
    }
}
