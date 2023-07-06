using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter
{
    public class CreationDateOptions : IDateOptions
    {
        public DateTime dateTime { get; set; }

        public void SetDate(string file)
        {
            dateTime= File.GetCreationTime(file);
        }
        public DateTime GetCreateDate(string file)
        {
            return File.GetCreationTime(file);
        }
        public DateTime GetModifiedDate(string file)
        {
            return File.GetLastWriteTime(file);
        }
        public DateTime GetAccessDate(string file)
        {
            return File.GetLastAccessTime(file);
        }
    }
}
