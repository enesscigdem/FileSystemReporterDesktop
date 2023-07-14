using FileOrbis___File_System_Reporter.File_İnformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter.File_Process
{
    public interface IFileOperation
    {
        void Execute(string sourcePath, string targetPath, string selectedFileName, bool overwriteCheck, bool copyPermission, bool emptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions);
    }
}
