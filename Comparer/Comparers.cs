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
}
