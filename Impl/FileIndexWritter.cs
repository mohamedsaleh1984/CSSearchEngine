using CSSearchEngine.Abstraction;
using CSSearchEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSSearchEngine.Impl
{
    public class FileIndexWritter : IFileIndexWritter
    {
        /// <summary>
        /// Write file index as Json
        /// </summary>
        /// <param name="fileIndex"></param>
        public void Write(FileIndex fileIndex)
        {
            string strIndexFile = Path.Combine(Path.GetDirectoryName(fileIndex.OriginalPath), "_index");
            if (!Directory.Exists(strIndexFile))
            {
                Directory.CreateDirectory(strIndexFile);
            }

            string strGuid = Guid.NewGuid().ToString("N") + ".json";
            string strIndexFilePath = Path.Combine(strIndexFile,strGuid);
            string strJson = JsonSerializer.Serialize(fileIndex);
            File.WriteAllText(strIndexFilePath, strJson);
        }
    }
}
