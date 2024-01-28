namespace CSSearchEngine.Extensions
{
    public static class StringExtensions
    {
        public static bool EndsWith(this string str, string[] acceptedExtensions)
        {
            foreach (var ext in acceptedExtensions)
            {
                if (str.ToLower().EndsWith(ext.ToLower()))
                    return true;
            }
            return false;
        }
        public static bool StartsWith(this string str, string[] acceptedExtensions)
        {
            foreach (var ext in acceptedExtensions)
            {
                if (str.ToLower().StartsWith(ext.ToLower()))
                    return true;
            }
            return false;
        }
        public static string TrimExtended(this string strInput)
        {
            string[] str = { ".", "-", "*", "!", "]", "[", ";", ")", "(", "_", "=", "/", "\\", "?",":","#",",","\"",","};
            string strOutput = strInput;

            strOutput = strOutput.Replace("\t", "");
            strOutput = strOutput.Replace("\n", "");
            strOutput = strOutput.Replace("\r", "");

            while (strOutput.EndsWith(str))
                strOutput = strOutput.Substring(0, strOutput.Length - 1);

            while (strOutput.StartsWith(str))
                strOutput = strOutput.Substring(1, strOutput.Length-1);

            strOutput = strOutput.Trim();

            return strOutput;
        }
    }
}