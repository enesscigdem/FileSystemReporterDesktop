﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter
{
    public class ModifiedDateOptions : IDateOptions
    {
        public DateTime SetDate(string file)
        {
            return File.GetLastWriteTime(file);
        }
    }
}
