using CSSearchEngine.Abstraction;

namespace CSSearchEngine.Impl
{

    public class LevenshteinDistance : ILevenshteinDistance
    {
        private const int ResultsToPresent = 10;
        private int CalculateLevenshteinDistance(string str1, string str2)
        {
            int lenStr1 = str1.Length + 1;
            int lenStr2 = str2.Length + 1;

            // Initialize a matrix to store the distances
            int[,] matrix = new int[lenStr1, lenStr2];

            // Fill in the matrix with initial values
            for (int i = 0; i < lenStr1; i++)
            {
                matrix[i, 0] = i;
            }

            for (int j = 0; j < lenStr2; j++)
            {
                matrix[0, j] = j;
            }

            // Calculate the distances
            for (int i = 1; i < lenStr1; i++)
            {
                for (int j = 1; j < lenStr2; j++)
                {
                    int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;
                    matrix[i, j] = Math.Min(
                        matrix[i - 1, j] + 1,      // deletion
                        Math.Min(
                            matrix[i, j - 1] + 1,   // insertion
                            matrix[i - 1, j - 1] + cost  // substitution
                        )
                    );
                }
            }

            return matrix[lenStr1 - 1, lenStr2 - 1];
        }

        public List<string> ClosetWords(string str1,List<string> words)
        {
            List<Tuple<string, int>> distances = new List<Tuple<string, int>>();
            // Calculate distances and store them in a list of tuples
            for (int i = 0; i < words.Count - 1; i++)
            {
                int distance = CalculateLevenshteinDistance(str1, words[i]);
                distances.Add(new Tuple<string, int>(words[i], distance));
            }

            // Sort the list based on distance
            distances.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            //Get Top 10 Words
            distances = distances.Take(ResultsToPresent).ToList();

            //return words
            return distances.Select(x => x.Item1).ToList(); 
        }
    }
}
