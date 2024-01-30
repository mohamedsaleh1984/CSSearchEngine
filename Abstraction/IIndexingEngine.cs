using CSSearchEngine.Model;

namespace CSSearchEngine.Abstraction
{
    public interface IIndexingEngine
	{
        string CreateSearchableFile(string originalFile);
        Dictionary<string, int> CreateFrequenceTable(string filePath);
        Dictionary<string, int> SortFrequencyTableByFrequency(Dictionary<string, int> FrequencyTable);
        void CreateTokenSpace(List<FileIndex> fileIndices);
        void StartIndexing(string strContent);
    }
}
