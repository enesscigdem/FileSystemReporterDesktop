using FileOrbis___File_System_Reporter.Date_Process;
using FileOrbis___File_System_Reporter.DateOptions;
using FileOrbis___File_System_Reporter.File_İnformation;
using FileOrbis___File_System_Reporter.File_Process;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOrbis___File_System_Reporter
{
    public class CopyProcess : IFileOperation
    {
        public void Execute(string sourcePath, string targetPath, string selectedFileName, bool overwriteCheck, bool emptyFoldersCheck, bool copyPermission, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions)
        {
            try
            {
                targetPath = targetPath + "\\" + selectedFileName;
                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);

                foreach (Folderİnformation dirPath in folderInformations)
                {
                    string targetDirPath = dirPath.FolderPath.Replace(sourcePath, targetPath);

                    if (emptyFoldersCheck || Directory.GetFiles(dirPath.FolderPath).Length > 0 || Directory.GetDirectories(dirPath.FolderPath).Length > 0)
                    {
                        fileDate = dateOptions.SetDate(dirPath.FolderPath);

                        if (fileDate > selectedDate)
                            Directory.CreateDirectory(targetDirPath);
                    }
                }
                foreach (Fileİnformation newPath in fileInformations)
                {
                    fileDate = dateOptions.SetDate(newPath.FilePath);

                    if (fileDate > selectedDate && overwriteCheck)
                        File.Copy(newPath.FilePath, newPath.FilePath.Replace(sourcePath, targetPath), true);
                    else if (fileDate > selectedDate && !overwriteCheck)
                        File.Copy(newPath.FilePath, newPath.FilePath.Replace(sourcePath, targetPath), false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
