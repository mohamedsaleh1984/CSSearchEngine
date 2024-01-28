using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Extensions
{
    public static class ListExtensions
    {
        public static bool ContainsAll<T>(this List<T> list, List<T> listOfStrings)
        {
            HashSet<T> set = new HashSet<T>(list);
            for(int i = 0;i < listOfStrings.Count(); i++)
            {
                if (!set.Contains(listOfStrings[i]))
                    return false;
            }
            return true;
        }

        public static bool ContainsAny<T>(this List<T> list, List<T> listOfStrings)
        {
            HashSet<T> set = new HashSet<T>(list);
            for (int i = 0; i < listOfStrings.Count(); i++)
            {
                if (set.Contains(listOfStrings[i]))
                    return true;
            }
            return false;
        }
    }
}
