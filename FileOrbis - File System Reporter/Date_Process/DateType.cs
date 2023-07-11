using FileOrbis___File_System_Reporter.DateOptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter.Date_Process
{
    public class DateType
    {
        public IDateOptions GetDateType(string dateType)
        {
            IDateOptions result;
            switch (dateType)
            {
                case "Created":
                    result = new CreatedDateOption();
                    break;
                case "Modified":
                    result = new ModifiedDateOptions();
                    break;
                case "Accessed":
                    result = new AccessedDateOption();
                    break;
                default:
                    throw new ArgumentException("Invalid date type.");
            }
            return result;
        }
    }  // bu cs. bana bir obje döndürecek, bu döndürdüğü obje de örneğin created, created ise CreatedDateOption fonksiyonu çalışacak
}
