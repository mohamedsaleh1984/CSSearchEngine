﻿using CSSearchEngine.Abstraction;
using CSSearchEngine.Comparer;
using CSSearchEngine.Extensions;
using CSSearchEngine.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSSearchEngine.Impl
{
    public class IndexingEngine : IIndexingEngine
    {
        private const int TopNTerms = 100;
        private static FileManip fileManip = new FileManip();
        private static FileIndexWritter indexWritter = new FileIndexWritter();

        /// <summary>
        /// Create Frequency Table
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Dictionary<string, int> CreateFrequenceTable(string filePath)
        {
            Dictionary<string, int> freqTable = new();
            List<string> tokenz = GetTokens(filePath);
            foreach (var term in tokenz)
            {
                if (freqTable.ContainsKey(term))
                    freqTable[term]++;
                else
                    freqTable.Add(term, 1);
            }


            return freqTable;
        }

        /// <summary>
        /// Tokenize File 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<string> GetTokens(string filePath)
        {
            string strFileContent = File.ReadAllText(filePath);
            return strFileContent.Split(" ",
                                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.ToUpper())
                                .Where(TokenCriterias())
                                .ToList();
        }

        /// <summary>
        /// Sort Frequency Table.
        /// </summary>
        /// <param name="FrequencyTable"></param>
        /// <returns></returns>
        public Dictionary<string, int> SortFrequencyTableByFrequency(Dictionary<string, int> FrequencyTable)
        {
            Dictionary<string, int> freqTable = new Dictionary<string, int>();
            List<KeyValuePair<string, int>> list = FrequencyTable.Take(TopNTerms).ToList();
            list.Sort(new FrequencyComparerAsc());

            foreach (var item in list)
                freqTable.Add(item.Key, item.Value);

            return freqTable;
        }

        /// <summary>
        /// Create new file with Searchable content
        /// </summary>
        /// <param name="originalFile"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string CreateSearchableFile(string originalFile)
        {
            string strSearchableDir = Path.Combine(Path.GetDirectoryName(originalFile), "_searchable");
            if (!Directory.Exists(strSearchableDir))
            {
                Directory.CreateDirectory(strSearchableDir);
            }

            string strSearchableFile = Path.Combine(strSearchableDir, Path.GetFileName(originalFile));

            string strContent = File.ReadAllText(originalFile);

            string strNewContent = CleanTextContent(strContent);

            File.WriteAllText(strSearchableFile, strNewContent);

            return strSearchableFile;
        }

        /// <summary>
        /// Remove extra contact and retain only words with more than 3 letters.
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        private string CleanTextContent(string strContent)
        {
            List<string> allWords = strContent.Normalize().Split(new char[] { '\n', '\t', ' ' }).ToList();

            for (int i = 0; i < allWords.Count; i++)
                allWords[i] = allWords[i].TrimExtended();

            allWords = allWords.Where(x => x.Length > 3).ToList();

            string srtReturn = string.Join(" ", allWords);

            return srtReturn.ToString();
        }

        /// <summary>
        /// Indexing
        /// </summary>
        /// <param name="strDirectory"></param>
        public void StartIndexing(string strDirectory)
        {
            List<string> filesList = fileManip.GetFilesPaths(strDirectory);
            Dictionary<string, Dictionary<string, int>> docsFreqTable = new();

            foreach (var filePath in filesList)
            {
                string strFileName = Path.GetFileNameWithoutExtension(filePath);
                //Remove unnecassery chars from words like . ! ?
                string newFilePath = CreateSearchableFile(filePath);

                //Create Frequency Table
                var ft = CreateFrequenceTable(newFilePath);

                //Remove Unwanted words
                ft = RemoveExcluded(ft);

                //Sort The Freuency Table
                var sft = SortFrequencyTableByFrequency(ft);

                if (!sft.Any())
                    continue;
                
                //Write File Index...
                FileIndex fileIndex = CreateTextFileIndex(filePath, sft);
                indexWritter.Write(fileIndex);

                //For Debugging Purposes...
                docsFreqTable.Add(filePath, sft);
            }


            //Write Frequency to JSON file
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize(docsFreqTable, opt);
            File.WriteAllText("index.json", strJson);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="freqTable"></param>
        /// <returns></returns>
        private FileIndex CreateTextFileIndex(string strFilePath, Dictionary<string, int> freqTable)
        {
            double totalNumberOfToken = freqTable.Values.Sum();

            FileIndex fileIndex = new()
            {
                OriginalPath = strFilePath,
                FileName = Path.GetFileName(strFilePath),
                TotalnumberOfTokens = totalNumberOfToken,
                Tokens = freqTable.Keys.ToList()
            };

            List<TokenStat> TokenStats = new List<TokenStat>();

            foreach (var token in freqTable)
            {
                TokenStat tokenStat = new TokenStat();
                tokenStat.Token = token.Key;
                tokenStat.Frequency = token.Value;
                tokenStat.Percent = (token.Value/ totalNumberOfToken)*100;
                TokenStats.Add(tokenStat);
            }

            fileIndex.TokenStats = TokenStats;

            return fileIndex;
        }
        private Func<string, bool> TokenCriterias()
        {
            Func<string, bool> criList = x => x.Length > 3;
            return criList;
        }

        /// <summary>
        /// Remove excluded Token
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        public Dictionary<string, int> RemoveExcluded(Dictionary<string, int>  ft)
        {
            List<string> exluded = File.ReadAllLines(Path.Join(Directory.GetCurrentDirectory(), "exclude.txt")).ToList();
            exluded = exluded.Select(x => x.ToUpper()).ToList();

            Dictionary<string, int> freqTable = ft;
            for (int i = 0;i < exluded.Count(); i++)
            {
                if (freqTable.ContainsKey(exluded[i]))
                    freqTable.Remove(exluded[i]);
            }

            return freqTable;
        }

    }
}
