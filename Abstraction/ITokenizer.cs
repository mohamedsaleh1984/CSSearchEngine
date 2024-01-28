using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Abstraction
{
    public interface ITokenizer
    {
        string CleanTextContent(string strContent);
        List<string> GetTokens(string content);
    }
}
