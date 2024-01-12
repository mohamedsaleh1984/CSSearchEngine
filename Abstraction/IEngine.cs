using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Abstraction
{
    public interface IEngine
    {
        Task<List<Func<string, bool>>> CreateTokenCriterias();
        Task<Dictionary<string, int>> CreateFrequenceTable(string filePath);
        Task<Dictionary<string, int>> SortFrequencyTableByFrequency(Dictionary<string, int> FrequencyTable);
        Task<List<string>> GetTokens(string filePath);
    }
}
