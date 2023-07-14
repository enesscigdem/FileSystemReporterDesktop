using FileOrbis___File_System_Reporter.Date_Process;
using FileOrbis___File_System_Reporter.File_İnformation;
using FileOrbis___File_System_Reporter.File_Process;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOrbis___File_System_Reporter
{
    public class MoveProcess : IFileOperation
    {
        public void Execute(string sourcePath, string targetPath, string selectedFileName, bool overwriteCheck, bool copyPermission, bool emptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions)
        {
            foreach (Folderİnformation dirPath in folderInformations)
            {
                string targetDirPath = dirPath.FolderPath.Replace(sourcePath, targetPath);

                if ((emptyFoldersCheck || Directory.GetFileSystemEntries(dirPath.FolderPath).Count() > 0))
                {
                    fileDate = dateOptions.SetDate(dirPath.FolderPath);
                    if (fileDate > selectedDate)
                        Directory.CreateDirectory(targetDirPath);
                }
            }

            foreach (Fileİnformation newPath in fileInformations)
            {
                fileDate = dateOptions.SetDate(newPath.FilePath);
                if (fileDate > selectedDate)
                {
                    string newFilePath = newPath.FilePath.Replace(sourcePath, targetPath);
                    if (File.Exists(newFilePath))
                        File.Delete(newFilePath);
                    File.Move(newPath.FilePath, newFilePath);
                }
            }

        }
    }
}
