using CSSearchEngine.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Impl
{
    public class Engine : IEngine
    {
        public Task<Dictionary<string, int>> CreateFrequenceTable(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<List<Func<string, bool>>> CreateTokenCriterias()
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, int>> SortFrequencyTableByFrequency(Dictionary<string, int> FrequencyTable)
        {
            throw new NotImplementedException();
        }
        public Task<List<string>> GetTokens(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
