using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter.Date_Process
{
    public class DateType
    {
        public DateTime fileDate;
        public string file;
        public DateTime GetDateType(string dateType, string file)
        {
            DateTime result;
            switch (dateType)
            {
                case "Created":
                    result = File.GetCreationTime(file);
                    break;
                case "Modified":
                    result = File.GetLastWriteTime(file);
                    break;
                case "Accessed":
                    result = File.GetLastAccessTime(file);
                    break;
                default:
                    throw new ArgumentException("Invalid date type.");
            }
            return result;
        }

    }
}
