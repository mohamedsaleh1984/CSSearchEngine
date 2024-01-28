using CSSearchEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Comparer
{
    public class FrequencyComparerAsc : IComparer<KeyValuePair<string, int>>
    {
        public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
        {
            return x.Value == y.Value ? 0 : x.Value > y.Value ? 1 : -1;
        }
    }

    public class TokenComparer : IComparer<KeyValuePair<string, int>>
    {
        public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
        {
            return x.Key.CompareTo(y.Key);
        }
    }

    public class TokenStatComparerAsc : IComparer<TokenStat>
    {
        public int Compare(TokenStat? x, TokenStat? y)
        {
            return x.Percent == y.Percent ? 0 : x.Percent > y.Percent ? 1 : -1;
        }
    }
    public class TokenStatComparerDesc : IComparer<TokenStat>
    {
        public int Compare(TokenStat x, TokenStat y)
        {
            return x.Percent == y.Percent ? 0 : x.Percent > y.Percent ? -1 : 1;
        }
    }
}
