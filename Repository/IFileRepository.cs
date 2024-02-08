using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IFileRepository
    {
        public  Task<List<string>> GetAllFiles();
        
        public string GetUniqueFilePath(string fileName);
      
        public  bool DeleteFile(string fileName);

    }
}
