using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Abstraction
{
    internal interface ILevenshteinDistance
    {
        List<string> ClosetWords(string str1, List<string> strings);
    }
}
