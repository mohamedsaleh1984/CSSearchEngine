using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Check of all items in listOfitems exsist in List
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">Original List</param>
        /// <param name="listOfItems">Parameter</param>
        /// <returns></returns>
        public static bool ContainsAll<T>(this List<T> list, List<T> listOfItems)
        {
            HashSet<T> set = new HashSet<T>(list);
            for(int i = 0;i < listOfItems.Count(); i++)
            {
                if (!set.Contains(listOfItems[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check of any items in listOfitems exsist in List
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">Original List</param>
        /// <param name="listOfItems">Parameter</param>
        /// <returns></returns>
        public static bool ContainsAny<T>(this List<T> list, List<T> listOfItems)
        {
            HashSet<T> set = new HashSet<T>(list);
            for (int i = 0; i < listOfItems.Count(); i++)
            {
                if (set.Contains(listOfItems[i]))
                    return true;
            }
            return false;
        }
    }
}
