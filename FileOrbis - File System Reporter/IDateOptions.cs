using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter
{
    public interface IDateOptions
    {
        DateTime GetCreateDate(string file);
        DateTime GetModifiedDate(string file);
        DateTime GetAccessDate(string file);
        void SetDate(string file);
    }
}
