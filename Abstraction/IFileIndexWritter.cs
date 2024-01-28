using CSSearchEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSearchEngine.Abstraction
{
    public interface IFileIndexWritter
    {
        public void Write(FileIndex fileIndex);
    }
}
