﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter
{
    public class Fileİnformation
    {
        // classa çevirelim
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime FileCreateDate { get; set; }
        public DateTime FileModifiedDate { get; set; }
        public DateTime FileAccessDate { get; set; }
        //CreationDateOptions dateOptions { get; set; }
    }
}
