using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter
{
    public class ModifiedDateOptions : IDateOptions
    {
        public DateTime SetCreationDate(string file)
        {
            return File.GetCreationTime(file);
        }
        public DateTime SetModifiedDate(string file)
        {
            return File.GetLastWriteTime(file);
        }
        public DateTime SetAccessedDate(string file)
        {
            return File.GetLastAccessTime(file);
        }
    }
}
