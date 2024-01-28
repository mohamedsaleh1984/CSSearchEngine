using CSSearchEngine.Abstraction;
using CSSearchEngine.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Impl
{
    public class Tokenizer : ITokenizer
    {
        /// <summary>
        /// Tokenize File 
        /// </summary>
        /// <param name="content">Content</param>
        /// <returns></returns>
        public List<string> GetTokens(string content)
        {
            string strCleanedContent = CleanTextContent(content);
            return strCleanedContent.Split(" ",
                               StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                               .Select(x => x.ToUpper())
                               .ToList();
        }


        /// <summary>
        /// Remove extra contact and retain only words with more than 3 letters.
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public string CleanTextContent(string strContent)
        {
            List<string> allWords = strContent.Normalize().Split(new char[] { '\n', '\t', ' ' }).ToList();

            for (int i = 0; i < allWords.Count; i++)
                allWords[i] = allWords[i].TrimExtended();

            allWords = allWords.Where(x => x.Length > 3).ToList();

            string srtReturn = string.Join(" ", allWords);

            return srtReturn.ToString();
        }
    }
}
