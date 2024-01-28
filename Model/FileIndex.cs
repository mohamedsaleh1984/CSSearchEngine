using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Model
{
    public class FileIndex
    {
        public string OriginalPath { get; set; }
        public string FileName { get; set; }
        public List<TokenStat> TokenStats { get; set; }
        public List<string> Tokens { get; set; }    
        public double TotalnumberOfTokens { get; set; }
    }
}
