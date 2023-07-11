using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter
{
    public interface IDateOptions
    {
        DateTime SetCreationDate(string file);
        DateTime SetModifiedDate(string file);
        DateTime SetAccessedDate(string file);
    }
}
